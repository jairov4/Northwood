using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Northwood.UI
{
	/// <summary>
	/// Lógica de Bootstraping para la aplicacion
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			IoC.Container = new AppIoC();
			MainWindow = new MainWindow();
			MainWindow.Show();
			CommandManager.AddExecutedHandler(App.Current.MainWindow, OnCommandExecuted);
			CommandManager.AddCanExecuteHandler(App.Current.MainWindow, OnCommandCanExecute);
		}

		private void OnCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			if (e.Command == ApplicationCommands.New)
			{
				var param = e.Parameter as string;
				if (param == Commands.BlankProject)
				{
					e.Handled = true;
					e.CanExecute = true;
					e.ContinueRouting = false;
				}
				else if (param == Commands.BlockDiagramDocument)
				{
					e.Handled = true;
					e.CanExecute = true;
					e.ContinueRouting = false;
				}
				else if (param == Commands.FSMDocument)
				{
					e.Handled = true;
					e.CanExecute = true;
					e.ContinueRouting = false;
				}
			}
		}

		private void OnCommandExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Command == ApplicationCommands.New)
			{
				var param = e.Parameter as string;
				if (param == Commands.BlankProject)
				{
					e.Handled = true;
					var manager = IoC.Container.Resolve<IProjectManager>();
					manager.CreateNew();
					var shell = IoC.Container.Resolve<IProjectShell>();
					shell.Show();
				}
				else if (param == Commands.FSMDocument)
				{
					e.Handled = true;
					var manager = IoC.Container.Resolve<IProjectManager>();
					var project = manager.CurrentProject;
					project.Documents.Add(new ProjectFSM());
				}
				else if (param == Commands.BlockDiagramDocument)
				{
					e.Handled = true;
					var manager = IoC.Container.Resolve<IProjectManager>();
					var project = manager.CurrentProject;
					project.Documents.Add(new ProjectBlockDiagram());
				}
			}
		}
	}
}
