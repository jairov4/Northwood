using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwood.UI
{
	public class EditorFactory : IEditorFactory
	{
		IProjectManager manager;
		IProjectShell shell;

		public EditorFactory(IProjectManager manager, IProjectShell shell)
		{
			this.manager = manager;
			this.shell = shell;
		}

		public void EditDocument(ProjectDocument document, bool reuse)
		{

		}
	}
}
