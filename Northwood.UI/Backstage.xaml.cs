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
	public partial class Backstage : UserControl, IView
	{
		public Backstage()
		{
			InitializeComponent();
		}

		public BackstageViewModel ViewModel
		{
			get { return DataContext as BackstageViewModel; }
			set { DataContext = value; }			
		}

		IViewModel IView.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = value as BackstageViewModel; }
		}
	}
}
