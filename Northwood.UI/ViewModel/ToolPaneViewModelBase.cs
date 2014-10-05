using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Northwood
{
	public abstract class ToolPaneViewModelBase : ViewModelBase, IToolPaneViewModel
	{
		private string _Title;

		public string Title
		{
			get { return _Title; }
			protected set { SetValue(ref _Title, value); }
		}

		private ImageSource _IconSource;

		public ImageSource IconSource
		{
			get { return _IconSource;}
			protected set { SetValue(ref _IconSource, value); }
		}
	}
}
