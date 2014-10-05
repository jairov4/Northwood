using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Northwood
{
	public interface IToolPaneViewModel : IViewModel
	{
		string Title { get; }

		ImageSource IconSource { get; }
	}
}
