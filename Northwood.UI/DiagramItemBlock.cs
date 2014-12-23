using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Northwood.UI
{
	public class DiagramItemBlock : DiagramItem
	{
		static DiagramItemBlock()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramItemBlock), new FrameworkPropertyMetadata(typeof(DiagramItemBlock)));
		}

		public DiagramItemBlock()
		{
			LeftItems = new ObservableCollection<object>();
			RightItems = new ObservableCollection<object>();
		}

		
		public Thickness ContentMargin
		{
			get { return (Thickness)GetValue(ContentMarginProperty); }
			set { SetValue(ContentMarginProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ContentMargin.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ContentMarginProperty =
			DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(DiagramItemBlock), new FrameworkPropertyMetadata(new Thickness(5)));

		
		public IList LeftItems
		{
			get { return (IList)GetValue(LeftItemsProperty); }
			set { SetValue(LeftItemsProperty, value); }
		}

		// Using a DependencyProperty as the backing store for LeftItems.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty LeftItemsProperty =
			DependencyProperty.Register("LeftItems", typeof(IList), typeof(DiagramItemBlock), new FrameworkPropertyMetadata(null));

		public IList RightItems
		{
			get { return (IList)GetValue(RightItemsProperty); }
			set { SetValue(RightItemsProperty, value); }
		}

		// Using a DependencyProperty as the backing store for RightItems.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty RightItemsProperty =
			DependencyProperty.Register("RightItems", typeof(IList), typeof(DiagramItemBlock), new FrameworkPropertyMetadata(null));

		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemTemplateProperty =
			DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(DiagramItemBlock), new FrameworkPropertyMetadata(null));

		public DataTemplateSelector ItemTemplateSelector
		{
			get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
			set { SetValue(ItemTemplateSelectorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ItemTemplateSelector.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemTemplateSelectorProperty =
			DependencyProperty.Register("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(DiagramItemBlock), new FrameworkPropertyMetadata(null));

	}
}