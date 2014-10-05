using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Northwood.UI
{
	public class EditorManager : IEditorManager
	{
		ObservableCollection<IEditor> documents;
		ReadOnlyObservableCollection<IEditor> roDocuments;
		List<IEditorFactory> factories;

		public EditorManager(IProjectManager manager, IEnumerable<IEditorFactory> factories)
		{
			this.documents = new ObservableCollection<IEditor>();
			this.roDocuments = new ReadOnlyObservableCollection<IEditor>(documents);
			this.factories = new List<IEditorFactory>(factories);
		}

		public IEditor EditDocument(ProjectDocument document)
		{
			if (factories == null) return null;
			foreach (var item in factories)
			{
				if (!item.CanEdit(document)) continue;
				var editor = item.CreateEditor(document);
				documents.Add(editor);
				return editor;
			}
			return null;
		}

		public void CloseEditor(IEditor editor)
		{
			documents.Remove(editor);
		}

		public ReadOnlyObservableCollection<IEditor> Documents { get { return roDocuments; } }
	}
}
