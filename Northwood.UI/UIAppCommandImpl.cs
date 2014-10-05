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
		IEditorManager editorManager;

		public UIAppCommandImpl(Window window, IProjectManager manager, IProjectShell shell, IEditorManager editorManager)
		{
			this.window = window;
			this.shell = shell;
			this.manager = manager;
			this.editorManager = editorManager;
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
				var valid = new[] { Commands.BlankProject, Commands.BlockDiagramDocument, Commands.FSMDocument };
				if (valid.Contains(param))
				{
					e.Handled = true;
					e.CanExecute = true;
					e.ContinueRouting = false;
				}
			} 
			else if(e.Command == ApplicationCommands.Open)
			{
				var suitable = e.Parameter is ProjectDocument;
				if(suitable)
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
				HandleNew(e);
			}
			else if(e.Command == ApplicationCommands.Open)
			{
				HandleOpen(e);
			}
		}

		private void HandleNew(ExecutedRoutedEventArgs e)
		{
			var param = e.Parameter as string;
			if (param == Commands.BlankProject)
			{
				e.Handled = true;
				manager.CreateNewProject();
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
				if (manager.CurrentProject == null)
				{
					manager.CreateNewProject();
				}
				manager.AddBlockDiagram();
			}
			if (e.Handled)
			{
				shell.CloseBackstage();
			}
		}

		private void HandleOpen(ExecutedRoutedEventArgs e)
		{
			var param = e.Parameter as ProjectDocument;
			if (param != null)
			{
				e.Handled = true;
				editorManager.EditDocument(param);
			}
		}

		private void NotifyToUser(string p)
		{
			MessageBox.Show(p);
		}
	}
}
