using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Northwood
{
	public class ProjectRoot : ObservableObject
	{
		private string _Name;

		public string Name
		{
			get { return _Name; }
			set { SetValue("Name", ref _Name, value); }
		}

		ObservableCollection<ProjectDocument> _Documents = new ObservableCollection<ProjectDocument>();
		public ObservableCollection<ProjectDocument> Documents
		{
			get { return _Documents; }
		}

	}
}
