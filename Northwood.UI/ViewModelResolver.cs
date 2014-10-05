using Autofac;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Northwood.UI
{
	/// <summary>
	/// Resolves a ViewModel instance in same Assembly and same namespace for IView
	/// which Autowire property is setted up to true. It offers ViewModel to View conversion
	/// to be used in XAML too.
	/// </summary>
	public class ViewModelResolver : DependencyObject, IValueConverter
	{
		private static IContainer container;

		public static IContainer Container
		{
			set { container = value; }
		}

		private static ILogger log;

		private static ILogger Log
		{
			get
			{
				if (log == null) log = container.Resolve<ILogger>();
				return log;
			}
		}

		public static void SetAutowire(UIElement element, Boolean value)
		{
			element.SetValue(AutowireProperty, value);
		}
		public static Boolean GetIsBubbleSource(UIElement element)
		{
			return (Boolean)element.GetValue(AutowireProperty);
		}

		// Using a DependencyProperty as the backing store for Autowire.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty AutowireProperty =
			DependencyProperty.RegisterAttached("Autowire", typeof(bool), typeof(ViewModelResolver), new PropertyMetadata(false, Autowire_PropertyChanged));

		private static void Autowire_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (!(d is IView && d is FrameworkElement))
			{
				return;
			}
			var fe = d as FrameworkElement;
			if (e.NewValue != null && (bool)e.NewValue == true)
			{
				fe.Loaded += ViewModelResolver_Loaded;
			}
			else
			{
				fe.Loaded -= ViewModelResolver_Loaded;
			}
		}

		private static void ViewModelResolver_Loaded(object sender, RoutedEventArgs e)
		{
			if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(sender as DependencyObject)) return;
			var view = sender as IView;
			if (view.ViewModel == null)
			{
				view.ViewModel = ResolveViewModel(view);
				Log.Debug("View Model resolved for {0}", new[] { view });
			}
		}

		public static IViewModel ResolveViewModel(IView view)
		{
			var type = view.GetType();
			var className = type.FullName + "Model";
			var typeVm = type.Assembly.GetType(className);
			var vm = container.Resolve(typeVm) as IViewModel;
			return vm;
		}

		public static IView ResolveView(IViewModel model)
		{
			var type = model.GetType();
			var className = type.FullName.Remove(type.FullName.Length - 5);
			var typeVm = type.Assembly.GetType(className);
			var vm = container.Resolve(typeVm) as IView;
			return vm;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is IViewModel) return ViewModelResolver.ResolveView(value as IViewModel);
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is IView) return ViewModelResolver.ResolveViewModel(value as IView);
			return value;
		}
	}
}
