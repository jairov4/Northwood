using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwood
{
	public interface IEditorFactory
	{
		bool CanEdit(ProjectDocument j);

		IEditor CreateEditor(ProjectDocument j);
	}
}
