using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

	public class DiagramItem : HeaderedItemsControl
	{
		static DiagramItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramItem), new FrameworkPropertyMetadata(typeof(DiagramItem)));
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

		// Using a DependencyProperty as the backing store for Z.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ZProperty =
			DependencyProperty.Register("Z", typeof(int), typeof(DiagramItem), new FrameworkPropertyMetadata(0));

		protected override bool IsItemItsOwnContainerOverride(object item) { return item is DiagramItemPort; }

		protected override DependencyObject GetContainerForItemOverride() { return new DiagramItemPort(); }
	}

	public class DiagramItemPort : HeaderedContentControl
	{
		static DiagramItemPort()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramItemPort), new FrameworkPropertyMetadata(typeof(DiagramItemPort)));
		}
	}
}
