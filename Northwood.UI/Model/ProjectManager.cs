using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace Northwood.UI
{
	[Export(typeof(IProjectManager))]
	public class ProjectManager : ViewModel, IProjectManager
	{

		public ProjectManager()
		{
		}

		public void CreateNew()
		{
			if (CurrentProject != null)
			{
				CloseProject();
			}
			CurrentProject = new ProjectRoot();
		}

		private void CloseProject()
		{
			throw new NotImplementedException();
		}

		ProjectRoot _CurrentProject;
		public ProjectRoot CurrentProject
		{
			get { return _CurrentProject; }
			private set { SetValue("CurrentProject", ref _CurrentProject, value); }
		}

		public ProjectBlockDiagram AddBlockDiagram()
		{
			var bd = new ProjectBlockDiagram();
			CurrentProject.Documents.Add(bd);
			return bd;
		}
	}
}
