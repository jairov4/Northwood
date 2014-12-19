using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Northwood.UI
{
	public class Diagram : MultiSelector
	{
		static Diagram()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Diagram), new FrameworkPropertyMetadata(typeof(Diagram)));
		}

		protected override bool IsItemItsOwnContainerOverride(object item) { return item is DiagramItem; }

		protected override DependencyObject GetContainerForItemOverride() { return new DiagramItem(); }
	}

	public class DiagramItem : ContentControl
	{
		static DiagramItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramItem), new FrameworkPropertyMetadata(typeof(DiagramItem)));
		}

		public DiagramItem()
		{
			_Items.CollectionChanged += _Items_CollectionChanged;
		}

		public double X
		{
			get { return (double)GetValue(XProperty); }
			set { SetValue(XProperty, value); }
		}

		// Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty XProperty =
			DependencyProperty.Register("X", typeof(double), typeof(DiagramItem), new FrameworkPropertyMetadata(0.0, X_OnPropertyChanged));

		private static void X_OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var This = d as DiagramItem;
			Canvas.SetLeft(This, (double)e.NewValue);
		}

		public double Y
		{
			get { return (double)GetValue(YProperty); }
			set { SetValue(YProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty YProperty =
			DependencyProperty.Register("Y", typeof(double), typeof(DiagramItem), new FrameworkPropertyMetadata(0.0, Y_OnPropertyChanged));

		private static void Y_OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var This = d as DiagramItem;
			Canvas.SetTop(This, (double)e.NewValue);
		}


		public int Z
		{
			get { return (int)GetValue(ZProperty); }
			set { SetValue(ZProperty, value); }
		}



		public Thickness ContentMargin
		{
			get { return (Thickness)GetValue(ContentMarginProperty); }
			set { SetValue(ContentMarginProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ContentMargin.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ContentMarginProperty =
			DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(DiagramItem), new FrameworkPropertyMetadata(new Thickness(5)));


		// Using a DependencyProperty as the backing store for Z.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ZProperty =
			DependencyProperty.Register("Z", typeof(int), typeof(DiagramItem), new FrameworkPropertyMetadata(0));

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (Template == null) return;
			var pnlLeft = Template.FindName("pnlLeft", this) as Panel;
			var pnlRight = Template.FindName("pnlRight", this) as Panel;
			if (pnlLeft == null) return;
			foreach (UIElement a in Items)
			{
				pnlLeft.Children.Add(a);
			}
		}

		void _Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (Template == null) return;
			var pnlLeft = Template.FindName("pnlLeft", this) as Panel;
			var pnlRight = Template.FindName("pnlRight", this) as Panel;
			if (pnlLeft == null) return;
			foreach (UIElement a in e.OldItems)
			{
				pnlLeft.Children.Remove(a);
				pnlRight.Children.Remove(a);
			}
			foreach (UIElement a in e.NewItems)
			{
				pnlLeft.Children.Add(a);
			}
		}

		ObservableCollection<UIElement> _Items = new ObservableCollection<UIElement>();

		public ObservableCollection<UIElement> Items { get { return _Items; } }

	}

	public class DiagramItemPort : HeaderedContentControl
	{
		static DiagramItemPort()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramItemPort), new FrameworkPropertyMetadata(typeof(DiagramItemPort)));
		}
	}
}
