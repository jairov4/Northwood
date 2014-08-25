using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Northwood
{
	public abstract class ObservableObject : INotifyPropertyChanged
	{
		protected void RaisePropertyChanged([CallerMemberName] string name = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string name = null)
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
