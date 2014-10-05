using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Northwood.UI
{
	public enum ShellState
	{
		ProjectEditor, Backstage
	}

	public class ProjectShellViewModel : ViewModelBase, IProjectShell
	{
		private ShellState _State;

		public ShellState State
		{
			get { return _State; }
			set { SetValue(ref _State, value); }
		}

		private IProjectManager _Manager;

		public IProjectManager ProjectManager
		{
			get { return _Manager; }
			private set { SetValue(ref _Manager, value); }
		}

		IEditorManager _EditorManager;

		public IEditorManager EditorManager
		{
			get { return _EditorManager; }
			private set { SetValue(ref _EditorManager, value); }
		}

		ObservableCollection<IToolPaneViewModel> _Tools;
		public ObservableCollection<IToolPaneViewModel> Tools
		{
			get { return _Tools; }
		}
		
		public void CloseBackstage()
		{
			State = ShellState.ProjectEditor;
		}

		public ProjectShellViewModel(IProjectManager manager, IEditorManager eManager, IEnumerable<IToolPaneViewModel> tools)
		{
			_Manager = manager;
			_EditorManager = eManager;
			State = ShellState.Backstage;
			_Tools = new ObservableCollection<IToolPaneViewModel>(tools);
			manager.PropertyChanged += manager_PropertyChanged;
		}

		void manager_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e.PropertyName == "CurrentProject")
			{
				State = ProjectManager.CurrentProject == null ? ShellState.Backstage : ShellState.ProjectEditor;
			}
		}
	}
}
