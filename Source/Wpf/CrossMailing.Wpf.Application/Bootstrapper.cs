﻿using System.Windows;
using CrossMailing.Wpf.Application.Shell;
using CrossMailing.Wpf.Common.Events;
using CrossMailing.Wpf.Mail;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;

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

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog) ModuleCatalog;

            moduleCatalog.AddModule(typeof (MailModule));
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();

            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ActivateMailModuleEvent>().Publish(new ActivateMailModulePayload());
        }
    }
}