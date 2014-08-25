using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Northwood.UI
{
	public interface IProjectManager : INotifyPropertyChanged
	{
		void CreateNewProject();

		ProjectRoot CurrentProject { get; }
	}
}
