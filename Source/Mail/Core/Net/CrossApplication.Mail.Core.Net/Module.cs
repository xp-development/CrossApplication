using System.Threading.Tasks;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Mail.Core.Navigation;
using MailMessaging.Plain.Contracts;
using MailMessaging.Plain.Contracts.Services;
using MailMessaging.Plain.Core;
using MailMessaging.Plain.Core.Services;

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
            _container.RegisterType<ITcpClient, TcpClient>();
            _container.RegisterType<ITagService, TagService>();
            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            return Task.FromResult(false);
        }

        private readonly IContainer _container;
    }
}