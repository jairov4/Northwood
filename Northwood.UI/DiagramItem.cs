using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Northwood.UI
{
	public abstract class DiagramItem : ContentControl
	{
		
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
			var This = (DiagramItem)d;
			if (This.XPropertyChanged != null) This.XPropertyChanged(This, e);
		}

		public event PropertyChangedCallback XPropertyChanged;

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
			var This = (DiagramItem)d;
			if (This.YPropertyChanged != null) This.YPropertyChanged(This, e);
		}
		
		public event PropertyChangedCallback YPropertyChanged;

		public int Z
		{
			get { return (int)GetValue(ZProperty); }
			set { SetValue(ZProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Z.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ZProperty =
			DependencyProperty.Register("Z", typeof(int), typeof(DiagramItem), new FrameworkPropertyMetadata(0));

		public static readonly DependencyProperty IsSelectedProperty = Selector.IsSelectedProperty.AddOwner(typeof(DiagramItem), new FrameworkPropertyMetadata(false, OnIsSelectedChanged));

		private static void OnIsSelectedChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
		{
			var item = (DiagramItem)target;
			item.OnIsSelectedChanged((bool)e.OldValue, (bool)e.NewValue);
		}

		protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue)
		{
			RaiseEvent(newValue
				? new RoutedEventArgs(Selector.SelectedEvent, this)
				: new RoutedEventArgs(Selector.UnselectedEvent, this));
		}

		public bool IsSelected
		{
			get
			{
				return (bool)GetValue(IsSelectedProperty);
			}
			set
			{
				SetValue(IsSelectedProperty, value);
			}
		}
	}
}
