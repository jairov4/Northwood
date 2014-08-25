using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Northwood.UI
{
	/// <summary>
	/// Lógica de Bootstraping para la aplicacion
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			var builder = new ContainerBuilder();
			var wnd = new MainWindow();
			
			Bootstrap(builder, wnd);

			var container = builder.Build();

			var cmd = container.Resolve<UIAppCommandImpl>();
			cmd.RegisterCommandHandlers();

			wnd.Shell.ViewModel = container.Resolve<ProjectShellViewModel>();

			MainWindow = wnd;
			wnd.Show();
		}

		private void Bootstrap(ContainerBuilder builder, Window wnd)
		{
			builder.RegisterType<ProjectManager>().As<IProjectManager>().SingleInstance();
			builder.RegisterType<EditorFactory>().As<IEditorFactory>().SingleInstance();
			builder.RegisterType<ProjectShellViewModel>().As<IProjectShell>().As<ProjectShellViewModel>().SingleInstance();
			builder.RegisterType<UIAppCommandImpl>().SingleInstance();
			builder.RegisterType<LoggerTrace>().As<ILogger>().SingleInstance();
			builder.RegisterInstance(wnd).As<Window>().SingleInstance();
		}
	}
}
