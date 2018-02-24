using System.Threading.Tasks;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Mail.Contracts.Messaging;
using CrossApplication.Mail.Core.Messaging;
using CrossApplication.Mail.Core.Navigation;

namespace CrossApplication.Mail.Core
{
    [Module]
    public class Module : IModule
    {
        private readonly IContainer _container;

        public Module(IContainer container)
        {
            _container = container;
        }

        public Task InitializeAsync()
        {
            _container.RegisterInstance<IMainNavigationItem>(new MainNavigationItem("E-Mail", ViewKeys.Shell, "Email"));
            _container.RegisterInstance<ISettingsNavigationItem>(new MainNavigationItem("E-Mail", ViewKeys.Settings, "Email"));
            _container.RegisterType<IMailAccountManager, MailAccountManager>();
            _container.RegisterType<IMailContactManager, MailContactManager>();

            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            return Task.FromResult(false);
        }
    }
}