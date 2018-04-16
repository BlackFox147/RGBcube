using System.Windows;
using Caliburn.Micro;
using RGBcube.ViewModels;

namespace RGBcube
{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }

        //private IContainer _container;

        //protected override void Configure()
        //{
        //    var builder = new ContainerBuilder();
        //    //  register view models
        //    builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
        //        .Where(type => type.Name.EndsWith("ViewModel"))
        //        .AsSelf()
        //        .InstancePerDependency();
        //    //  register views
        //    builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
        //        .Where(type => type.Name.EndsWith("View"))
        //        .AsSelf()
        //        .InstancePerDependency();

        //    builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();

        //    _container = builder.Build();
        //}

        //protected override IEnumerable<Assembly> SelectAssemblies()
        //{
        //    return new[]
        //               {
        //           GetType().Assembly,
        //           typeof(MainWindowViewModel).Assembly,
        //           typeof(MainWindowView).Assembly
        //       };
        //}

        //public IContainer Bootstrap()
        //{
        //    var builder = new ContainerBuilder();

        //    builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
        //    //builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

        //    //builder.RegisterType<FileDataService>().As<IDataService>();
        //    //builder.RegisterType<fLookupProvider>().As<ILookupProvider<f>>();
        //    //builder.RegisterType<fGroupLookupProvider>().As<ILookupProvider<fGroup>>();
        //    //builder.RegisterType<fDataProvider>().As<IfDataProvider>();

        //    //builder.RegisterType<fEditViewModel>().As<IfEditViewModel>();
        //    //builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
        //    //builder.RegisterType<MainViewModel>().AsSelf();

        //    builder.RegisterType<MainWindowViewModel>().AsSelf();
        //    return builder.Build();
        //}


        //private IContainer _container;

        //protected override void OnStartup(StartupEventArgs args)
        //{
        //    base.OnStartup(args);

        //    var builder = new ContainerBuilder();
        //    builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
        //    builder.RegisterType<WindowOpener>().As<IWindowOpener>().SingleInstance();
        //    builder.RegisterViewsAndViewModelsInAssembly();
        //    _container = builder.Build();

        //    MainWindow = _container.Resolve<MainWindowView>();
        //    MainWindow.Show();
        //}

        //protected override void OnExit(ExitEventArgs args)
        //{
        //    if (_container != null)
        //    {
        //        _container.Dispose();
        //    }

        //    base.OnExit(args);
        //}
    }
}
