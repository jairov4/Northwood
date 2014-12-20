using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

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
			LeftItems = new ObservableCollection<object>();
			RightItems = new ObservableCollection<object>();
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
			var This = (DiagramItem)d;
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
			var This = (DiagramItem)d;
			Canvas.SetTop(This, (double)e.NewValue);
		}

		public Thickness ContentMargin
		{
			get { return (Thickness)GetValue(ContentMarginProperty); }
			set { SetValue(ContentMarginProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ContentMargin.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ContentMarginProperty =
			DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(DiagramItem), new FrameworkPropertyMetadata(new Thickness(5)));

		public int Z
		{
			get { return (int)GetValue(ZProperty); }
			set { SetValue(ZProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Z.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ZProperty =
			DependencyProperty.Register("Z", typeof(int), typeof(DiagramItem), new FrameworkPropertyMetadata(0));


		public IList LeftItems
		{
			get { return (IList)GetValue(LeftItemsProperty); }
			set { SetValue(LeftItemsProperty, value); }
		}

		// Using a DependencyProperty as the backing store for LeftItems.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty LeftItemsProperty =
			DependencyProperty.Register("LeftItems", typeof(IList), typeof(DiagramItem), new FrameworkPropertyMetadata(null));

		public IList RightItems
		{
			get { return (IList)GetValue(RightItemsProperty); }
			set { SetValue(RightItemsProperty, value); }
		}

		// Using a DependencyProperty as the backing store for RightItems.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty RightItemsProperty =
			DependencyProperty.Register("RightItems", typeof(IList), typeof(DiagramItem), new FrameworkPropertyMetadata(null));

		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemTemplateProperty =
			DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(DiagramItem), new FrameworkPropertyMetadata(null));

		public DataTemplateSelector ItemTemplateSelector
		{
			get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
			set { SetValue(ItemTemplateSelectorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ItemTemplateSelector.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemTemplateSelectorProperty =
			DependencyProperty.Register("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(DiagramItem), new FrameworkPropertyMetadata(null));

	}

	public class DiagramItemPort : HeaderedContentControl
	{
		static DiagramItemPort()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramItemPort), new FrameworkPropertyMetadata(typeof(DiagramItemPort)));
		}
	}
}
