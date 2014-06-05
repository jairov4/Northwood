using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwood.UI
{
	public abstract class IoC
	{
		public static IoC Container { get; set; }

		public abstract T Resolve<T>();

		public abstract void Register(object instance);

		public abstract IEnumerable<T> ResolveAll<T>();
	}

	public class AppIoC : IoC
	{
		CompositionContainer container;

		public AppIoC()
		{
			//An aggregate catalog that combines multiple catalogs
			var catalog = new AggregateCatalog();
			//Adds all the parts found in the same assembly as the Program class
			catalog.Catalogs.Add(new AssemblyCatalog(typeof(App).Assembly));

			//Create the CompositionContainer with the parts in the catalog
			container = new CompositionContainer(catalog);
		}

		public override T Resolve<T>()
		{
			return container.GetExportedValue<T>();
		}

		public override void Register(object instance)
		{
			container.ComposeParts(instance);
		}

		public override IEnumerable<T> ResolveAll<T>()
		{
			return container.GetExportedValues<T>();
		}

	}
}
