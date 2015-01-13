using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Expression.Interactivity.Layout;

namespace Northwood.UI
{
	public class Diagram : MultiSelector
	{
		static Diagram()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Diagram), new FrameworkPropertyMetadata(typeof(Diagram)));
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is DiagramItem;
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new DiagramItemBlock();
		}

		protected override void PrepareContainerForItemOverride(DependencyObject element, object itemData)
		{
			base.PrepareContainerForItemOverride(element, itemData);
			var item = (DiagramItem)element;
			item.MouseLeftButtonDown += item_MouseLeftButtonDown;
			item.MouseMove += item_MouseMove;
			item.MouseLeftButtonUp += item_MouseLeftButtonUp;
			item.XPropertyChanged += item_XPropertyChanged;
			item.YPropertyChanged += item_YPropertyChanged;
			_dragInfo.Add(item, new DragInfo());
			UpdateItemPosition(item);
		}

		protected override void ClearContainerForItemOverride(DependencyObject element, object itemData)
		{
			base.ClearContainerForItemOverride(element, itemData);
			var item = (DiagramItem)element;
			item.MouseLeftButtonDown -= item_MouseLeftButtonDown;
			item.MouseMove -= item_MouseMove;
			item.MouseLeftButtonUp -= item_MouseLeftButtonUp;
			item.XPropertyChanged -= item_XPropertyChanged;
			item.YPropertyChanged -= item_YPropertyChanged;
			_dragInfo.Remove(item);
		}

		private void ToggleSelect(DiagramItem diagramItem)
		{
			if (diagramItem.IsSelected)
			{
				Unselect(diagramItem);
			}
			else
			{
				Select(diagramItem);
			}
		}

		private void Select(List<DiagramItem> items)
		{
			if (IsUpdatingSelectedItems) return;

			BeginUpdateSelectedItems();
			items.ForEach(a => SelectedItems.Add(ItemContainerGenerator.ItemFromContainer(a)));
			EndUpdateSelectedItems();
		}

		private void Select(DiagramItem itemBlock)
		{
			Select(new List<DiagramItem>() { itemBlock });
		}

		private void Unselect(DiagramItem itemBlock)
		{
			if (IsUpdatingSelectedItems) return;

			BeginUpdateSelectedItems();
			SelectedItems.Remove(ItemContainerGenerator.ItemFromContainer(itemBlock));
			EndUpdateSelectedItems();
		}

		protected override void OnSelectionChanged(SelectionChangedEventArgs e)
		{
			foreach (DependencyObject item in e.AddedItems)
			{
				var container = ContainerFromElement(item) as DiagramItemBlock;
				if (container != null)
				{
					var adornerLayer = AdornerLayer.GetAdornerLayer(container);
					adornerLayer.Add(new SimpleCircleAdorner(container));
				}
			}
			foreach (DependencyObject item in e.RemovedItems)
			{
				var container = ContainerFromElement(item) as DiagramItemBlock;
				if (container != null)
				{
					var adornerLayer = AdornerLayer.GetAdornerLayer(container);
					var adorners = adornerLayer.GetAdorners(container);
					foreach (var adorner in adorners)
					{
						if (!(adorner is SimpleCircleAdorner)) continue;
						adornerLayer.Remove(adorner);
					}
				}
			}
		}

		protected override void OnTemplateChanged(ControlTemplate oldTemplate, ControlTemplate newTemplate)
		{
			if (_content != null)
			{
				_content.MouseWheel -= OnMouseWheel;
				_content = null;
			}
			base.OnTemplateChanged(oldTemplate, newTemplate);
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			_content = Template.FindName("content", this) as FrameworkElement;
			if (_content == null) return;
			_scrollViewer = Template.FindName("scrollViewer", this) as ScrollViewer;

			_content.MouseWheel += OnMouseWheel;
		}

		private void OnMouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl)) return;

			e.Handled = true;
			_scaleFactor += e.Delta * 0.001;
			_content.LayoutTransform = new ScaleTransform(_scaleFactor, _scaleFactor);
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Middle)
			{
				e.Handled = true;
				IsPanning = true;
				_mousePositionPoint = e.GetPosition(this);
				CaptureMouse();
			}
			else
			{
				base.OnMouseDown(e);
			}
		}

		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			e.Handled = true;
			UnselectAll();
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Middle)
			{
				e.Handled = true;
				IsPanning = false;
				ReleaseMouseCapture();
			}
			else
			{
				base.OnMouseUp(e);
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.MiddleButton == MouseButtonState.Pressed)
			{
				if (!IsPanning) return;

				e.Handled = true;
				var pos = e.GetPosition(this);
				var delta = _mousePositionPoint - pos;
				_mousePositionPoint = pos;
				_scrollViewer.ScrollToHorizontalOffset(_scrollViewer.ContentHorizontalOffset + delta.X);
				_scrollViewer.ScrollToVerticalOffset(_scrollViewer.ContentVerticalOffset + delta.Y);
			}
			else
			{
				if (IsPanning) ReleaseMouseCapture();
				IsPanning = false;
				base.OnMouseMove(e);
			}
		}

		protected override void OnLostMouseCapture(MouseEventArgs e)
		{
			IsPanning = false;
			base.OnLostMouseCapture(e);
		}
		
		private void UpdateItemPosition(DiagramItem block)
		{
			if(!IsMeasureValid) Measure(Size.Empty);
			if (block is DiagramItemBlock)
			{
				Canvas.SetLeft(block, block.X - block.DesiredSize.Width / 2.0);
				Canvas.SetTop(block, block.Y - block.DesiredSize.Height / 2.0);
			}
			else
			{
				Canvas.SetLeft(block, block.X);
				Canvas.SetTop(block, block.Y);
			}
		}

		private void item_XPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var block = d as DiagramItemBlock;
			var newVal = (double)e.NewValue;
			if (block != null)
			{
				UpdateItemPosition(block);
			}
		}

		private void item_YPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var block = d as DiagramItemBlock;
			var newVal = (double)e.NewValue;
			if (block != null)
			{
				UpdateItemPosition(block);
			}
		}

		void item_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			var diagramItem = (DiagramItemBlock)sender;
			var info = _dragInfo[diagramItem];

			bool dragPreventsSelection = false;
			if (info.IsDragging)
			{
				var mpos = e.GetPosition(this);
				var deltaLengthSquared = ApplyMouseDrag(mpos, info, diagramItem);
				dragPreventsSelection = deltaLengthSquared > 3;
				info.IsDragging = false;
			}

			diagramItem.ReleaseMouseCapture();

			if (!dragPreventsSelection) ToggleSelect(diagramItem);

			e.Handled = true;
		}

		void item_MouseMove(object sender, MouseEventArgs e)
		{
			var diagramItem = (DiagramItemBlock)sender;
			var info = _dragInfo[diagramItem];
			if (info.IsDragging)
			{
				var mpos = e.GetPosition(this);
				var deltaLengthSquared = ApplyMouseDrag(mpos, info, diagramItem);
				if (deltaLengthSquared > 3 && !diagramItem.IsSelected)
				{
					Select(diagramItem);
				}
			}
		}

		private double ApplyMouseDrag(Point mpos, DragInfo info, DiagramItemBlock diagramItem)
		{
			var delta = (mpos - info.DragStartPosition) / _scaleFactor;
			diagramItem.X = info.ItemStartPosition.X + delta.X;
			diagramItem.Y = info.ItemStartPosition.Y + delta.Y;
			return delta.LengthSquared;
		}

		void item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var diagramItem = (DiagramItemBlock)sender;
			_dragInfo[diagramItem].DragStartPosition = e.GetPosition(this);
			_dragInfo[diagramItem].ItemStartPosition = new Point(diagramItem.X, diagramItem.Y);
			_dragInfo[diagramItem].IsDragging = true;
			diagramItem.CaptureMouse();
		}

		public bool IsPanning { get; private set; }

		private double _scaleFactor = 1.0;
		private Point _mousePositionPoint;
		private FrameworkElement _content;
		private ScrollViewer _scrollViewer;
		private readonly Dictionary<DiagramItem, DragInfo> _dragInfo = new Dictionary<DiagramItem, DragInfo>();

		private class DragInfo
		{
			public Point DragStartPosition { get; set; }
			public Point ItemStartPosition { get; set; }
			public bool IsDragging { get; set; }
		}
	}
}
