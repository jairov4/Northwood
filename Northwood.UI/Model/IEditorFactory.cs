using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwood.UI
{
	public interface IEditorFactory
	{
		IView EditDocument(ProjectDocument document, bool reuse);
	}
}
