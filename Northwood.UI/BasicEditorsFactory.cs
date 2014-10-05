using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwood.UI
{
	public class BasicEditorsFactory : IEditorFactory
	{
		public bool CanEdit(ProjectDocument j)
		{
			return j is ProjectBlockDiagram || j is ProjectFSM;
		}

		public IEditor CreateEditor(ProjectDocument j)
		{
			if(j is ProjectBlockDiagram)
			{
				var ed = new BlockDiagramView();
				ed.DataContext = j;
				return ed;
			}
			return null;
		}
	}
}
