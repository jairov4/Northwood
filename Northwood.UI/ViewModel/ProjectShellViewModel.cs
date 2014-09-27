using System;
using System.Collections.Generic;
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

	public class ProjectShellViewModel : ViewModel, IProjectShell
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
		
		public void CloseBackstage()
		{
			State = ShellState.ProjectEditor;
		}

		public ProjectShellViewModel(IProjectManager manager)
		{
			_Manager = manager;
			manager.PropertyChanged += manager_PropertyChanged;			
		}

		void manager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(e.PropertyName == "CurrentProject")
			{
				State = ProjectManager.CurrentProject == null ? ShellState.Backstage : ShellState.ProjectEditor;
			}
		}
	}
}
