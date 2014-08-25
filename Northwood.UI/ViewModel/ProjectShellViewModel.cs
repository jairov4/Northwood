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
		
		public void CloseBackstage()
		{
			State = ShellState.ProjectEditor;
		}

		public ProjectShellViewModel(IProjectManager manager)
		{
			State = manager.CurrentProject == null ? ShellState.Backstage : ShellState.ProjectEditor;
		}
	}
}
