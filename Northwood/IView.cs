using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwood
{
	public interface IView
	{
		IViewModel ViewModel { get; set; }
	}
}
