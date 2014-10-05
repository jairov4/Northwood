using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Northwood
{
	public interface IEditorManager
	{
		IEditor EditDocument(ProjectDocument document);

		void CloseEditor(IEditor editor);

		ReadOnlyObservableCollection<IEditor> Documents { get; }
	}
}
