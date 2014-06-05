using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Northwood
{
	public abstract class ObservableObject : INotifyPropertyChanged
	{
		protected void RaisePropertyChanged(string name)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		protected void SetValue<T>(string name, ref T backingField, T value)
		{
			var r = EqualityComparer<T>.Default;
			if (!r.Equals(backingField, value))
			{
				backingField = value;
				RaisePropertyChanged(name);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
