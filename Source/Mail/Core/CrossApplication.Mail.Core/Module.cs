using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Mail.Contracts.Messaging;
using CrossApplication.Mail.Core.Messaging;

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
            _container.RegisterType<IMailAccountManager, MailAccountManager>();
            _container.RegisterType<IMailManager, MailManager>();

            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            return Task.FromResult(false);
        }
    }
}