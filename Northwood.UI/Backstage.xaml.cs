using System;
using System.Collections.Generic;
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

namespace Northwood.UI
{
	/// <summary>
	/// Lógica de interacción para Backstage.xaml
	/// </summary>
	public partial class Backstage : UserControl
	{
		public Backstage()
		{
			InitializeComponent();
		}

		private void btnNewProject_Click(object sender, RoutedEventArgs e)
		{
			var projectManager = IoC.Container.Resolve<IProjectManager>();
			projectManager.CreateNew();
		}
	}
}
