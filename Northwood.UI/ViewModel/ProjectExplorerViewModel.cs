using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwood.UI
{
	public class ProjectExplorerViewModel : ViewModel
	{
		IProjectManager projectManager;

		public ProjectExplorerViewModel(IProjectManager manager)
		{
			projectManager = manager;
			projectManager.PropertyChanged += projectManager_PropertyChanged;
			Update();
		}

		private void Update()
		{
			Documents = new ReadOnlyObservableCollection<ProjectDocument>(projectManager.CurrentProject.Documents);
		}

		void projectManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "CurrentProject")
			{
				Update();
			}
		}

		
		public IReadOnlyCollection<ProjectDocument> Documents
		{
			get;
			private set;
		}

		private ProjectDocument _SelectedDocument;

		public ProjectDocument SelectedDocument
		{
			get { return _SelectedDocument; }
			set { SetValue(ref _SelectedDocument, value); }
		}

	}
}
