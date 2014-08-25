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
		protected readonly ILogger log;

		public ProjectManager(ILogger logger)
		{
			this.log = logger;
		}

		public void CreateNewProject()
		{
			if (CurrentProject != null)
			{
				log.Info("Closing current project");
				CloseProject();
			}
			CurrentProject = new ProjectRoot();
			log.Info("New Project created");
		}

		private void CloseProject()
		{
			CurrentProject = null;
		}

		ProjectRoot _CurrentProject;
		public ProjectRoot CurrentProject
		{
			get { return _CurrentProject; }
			private set { SetValue(ref _CurrentProject, value); }
		}

		public ProjectBlockDiagram AddBlockDiagram()
		{
			var bd = new ProjectBlockDiagram();
			CurrentProject.Documents.Add(bd);
			return bd;
		}
	}
}
