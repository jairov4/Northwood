using System;
using System.Collections.Generic;
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
	/// Lógica de interacción para BlockDiagramEditor.xaml
	/// </summary>
	public partial class BlockDiagramView : UserControl, IEditor, IView
	{
		public BlockDiagramView()
		{
			InitializeComponent();
		}

		public IViewModel ViewModel
		{
			get
			{
				return DataContext as IViewModel;
			}
			set
			{
				DataContext = value;
			}
		}
	}
}
