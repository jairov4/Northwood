using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Northwood.UI
{
	public class UIAppCommandImpl
	{
		Window window;
		IProjectManager manager;
		IProjectShell shell;

		public UIAppCommandImpl(Window window, IProjectManager manager, IProjectShell shell)
		{
			this.window = window;
			this.shell = shell;
			this.manager = manager;
		}

		public void RegisterCommandHandlers()
		{
			CommandManager.AddExecutedHandler(window, OnCommandExecuted);
			CommandManager.AddCanExecuteHandler(window, OnCommandCanExecute);
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
					manager.CreateNew();
				}
				else if (param == Commands.FSMDocument)
				{
					e.Handled = true;
					var project = manager.CurrentProject;
					project.Documents.Add(new ProjectFSM());
				}
				else if (param == Commands.BlockDiagramDocument)
				{
					e.Handled = true;
					var project = manager.CurrentProject;
					if (project != null)
					{
						project.Documents.Add(new ProjectBlockDiagram());
					}
					else
					{
						NotifyToUser("First create project, later you can add a Block Diagram");
					}
				}
				if (e.Handled)
				{
					shell.CloseBackstage();
				}
			}
		}

		private void NotifyToUser(string p)
		{
			MessageBox.Show(p);
		}
	}
}
