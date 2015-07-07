using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using CrossMailing.Common;
using CrossMailing.Wpf.Application.Shell;
using CrossMailing.Wpf.Common.Events;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;

namespace CrossMailing.Wpf.Application
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<RichShellView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            System.Windows.Application.Current.MainWindow = (Window) Shell;
            System.Windows.Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog { ModulePath = @".\Modules" };
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            ViewModelLocationProvider.SetDefaultViewModelFactory(t => Container.Resolve(t));
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType => Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewType.FullName, viewType.GetTypeInfo().Assembly.FullName)));

            Container.RegisterType<RichShellViewModel>();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();

            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ActivateModuleEvent>().Publish(new ActivateModulePayload(UniqueIdentifier.MailModuleIdentifier));
        }
    }
}