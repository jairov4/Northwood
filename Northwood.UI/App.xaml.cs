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
			ViewModelResolver.Container = container;

			var cmd = container.Resolve<UIAppCommandImpl>();
			cmd.RegisterCommandHandlers();

			MainWindow = wnd;
			wnd.Show();
		}

		private void Bootstrap(ContainerBuilder builder, Window wnd)
		{
			builder.RegisterType<ProjectManager>().As<IProjectManager>().SingleInstance();
			builder.RegisterType<EditorManager>().As<IEditorManager>().SingleInstance();			
			builder.RegisterType<UIAppCommandImpl>().SingleInstance();
			builder.RegisterType<LoggerTrace>().As<ILogger>().SingleInstance();
			builder.RegisterType<BasicEditorsFactory>().As<IEditorFactory>().SingleInstance();
			builder.RegisterInstance(wnd).As<Window>().SingleInstance();

			builder.RegisterType<ProjectShellViewModel>().As<ProjectShellViewModel>().As<IProjectShell>().SingleInstance();
			builder.RegisterType<ProjectExplorerViewModel>().As <ProjectExplorerViewModel>().As<IToolPaneViewModel>();
			builder.RegisterType<BackstageViewModel>();
			builder.RegisterType<BlockDiagramViewModel>();

			builder.RegisterType<ProjectExplorerView>();
			builder.RegisterType<BlockDiagramView>();
		}
	}
}
