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
			return new DiagramItem();
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

			items.ForEach(x =>
			{
				var adornerLayer = AdornerLayer.GetAdornerLayer(x);
				adornerLayer.Add(new SimpleCircleAdorner(x));
			});
		}

		private void Select(DiagramItem item)
		{
			Select(new List<DiagramItem>() { item });
		}

		private void Unselect(DiagramItem item)
		{
			if (IsUpdatingSelectedItems) return;

			BeginUpdateSelectedItems();
			SelectedItems.Remove(ItemContainerGenerator.ItemFromContainer(item));
			EndUpdateSelectedItems();

			var adornerLayer = AdornerLayer.GetAdornerLayer(item);
			var adorners = adornerLayer.GetAdorners(item);
			adornerLayer.Remove(adorners.First(x => x is SimpleCircleAdorner));
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
				var delta = (_mousePositionPoint - pos) * 0.08;
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

		protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
		{
			base.OnItemsChanged(e);
			if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace)
			{
				foreach (DiagramItem item in e.NewItems)
				{
					item.MouseLeftButtonDown += item_MouseLeftButtonDown;
					item.MouseMove += item_MouseMove;
					item.MouseLeftButtonUp += item_MouseLeftButtonUp;
					_dragInfo.Add(item, new DragInfo());
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
			{
				foreach (DiagramItem item in e.OldItems)
				{
					item.MouseLeftButtonDown -= item_MouseLeftButtonDown;
					item.MouseMove -= item_MouseMove;
					item.MouseLeftButtonUp -= item_MouseLeftButtonUp;
					_dragInfo.Remove(item);
				}
			}
			else if (e.Action == NotifyCollectionChangedAction.Reset)
			{
				foreach (DiagramItem item in Items)
				{
					item.MouseLeftButtonDown += item_MouseLeftButtonDown;
					item.MouseMove += item_MouseMove;
					item.MouseLeftButtonUp += item_MouseLeftButtonUp;
					_dragInfo.Add(item, new DragInfo());
				}
			}
		}

		void item_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			var diagramItem = (DiagramItem)sender;
			var info = _dragInfo[diagramItem];

			bool dragPreventsSelection = false;
			if (info.IsDragging)
			{
				var mpos = e.GetPosition(this);
				var delta = mpos - info.DragStartPosition;
				diagramItem.X = info.ItemStartPosition.X + delta.X;
				diagramItem.Y = info.ItemStartPosition.Y + delta.Y;
				dragPreventsSelection = delta.LengthSquared > 3;
			}

			info.IsDragging = false;
			diagramItem.ReleaseMouseCapture();

			if(!dragPreventsSelection) ToggleSelect(diagramItem);
		}

		void item_MouseMove(object sender, MouseEventArgs e)
		{
			var diagramItem = (DiagramItem)sender;
			var info = _dragInfo[diagramItem];
			if (info.IsDragging)
			{
				var mpos = e.GetPosition(this);
				var delta = mpos - info.DragStartPosition;
				diagramItem.X = info.ItemStartPosition.X + delta.X;
				diagramItem.Y = info.ItemStartPosition.Y + delta.Y;
			}
		}

		void item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var diagramItem = (DiagramItem)sender;
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
