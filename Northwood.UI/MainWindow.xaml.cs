using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace Northwood.UI
{
	/// <summary>
	/// Lógica de interacción para MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		public ProjectShellView Shell { get { return shell; } }
	}
}
