using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Layout;

namespace Northwood.UI
{
	/// <summary>
	/// Lógica de interacción para ProjectShell.xaml
	/// </summary>
	[Export(typeof(IProjectShell))]
	public partial class ProjectShell : UserControl, IProjectShell
	{
		public ProjectShell()
		{
			InitializeComponent();
			DataContext = IoC.Container.Resolve<IProjectManager>();
		}

		public void Show()
		{
			App.Current.MainWindow.Content = this;
		}
	}
}
