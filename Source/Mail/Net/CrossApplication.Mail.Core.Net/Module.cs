using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Net.Common.Navigation;
using CrossApplication.Core.Net.Contracts.Navigation;
using CrossApplication.Mail.Core.Navigation;
using MailMessaging.Plain.Contracts;
using MailMessaging.Plain.Core;

namespace CrossApplication.Mail.Core.Net
{
    [Module(Tag = ModuleTags.Infrastructure)]
    public class Module : IModule
    {
        public Module(IContainer container)
        {
            _container = container;
        }

        public Task InitializeAsync()
        {
            _container.RegisterInstance<IMainNavigationItem>(new MainNavigationItem("E-Mail", ViewKeys.Shell));
            _container.RegisterType<IMailMessenger, MailMessenger>();
            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            return Task.FromResult(false);
        }

        private readonly IContainer _container;
    }
}