using CrossApplication.Wpf.Common.Events;
using CrossApplication.Wpf.Contracts;
using Microsoft.Practices.Unity;
using Moq;
using Prism.Events;
using Prism.Regions;

namespace CrossApplication.Mail.Wpf.UnitTest._MailModule
{
    public abstract class TesterBase
    {
        protected UnityContainer TestContainer;
        protected Mock<IEventAggregator> EventAggregatorMock;
        protected Mock<IUnityContainer> UnityContainerMock;
        protected Mock<IRegionManager> RegionManagerMock;
        protected Mock<INavigationService> NavigationServiceMock;

        protected TesterBase()
        {
            TestContainer = new UnityContainer();

            EventAggregatorMock = new Mock<IEventAggregator>();
            UnityContainerMock = new Mock<IUnityContainer>();
            RegionManagerMock = new Mock<IRegionManager>();
            NavigationServiceMock = new Mock<INavigationService>();

            SetupEventAggregator();
            SetupUnityContainer();
            SetupRegionManager();
            SetupNavigationService();
        }

        protected virtual void SetupEventAggregator()
        {
            EventAggregatorMock.Setup(item => item.GetEvent<ActivateModuleEvent>()).Returns(new ActivateModuleEvent());
            EventAggregatorMock.Setup(item => item.GetEvent<InitializeModuleEvent>()).Returns(new InitializeModuleEvent());
            TestContainer.RegisterInstance(EventAggregatorMock.Object);
        }

        protected virtual void SetupUnityContainer()
        {
            TestContainer.RegisterInstance(UnityContainerMock.Object);
        }

        protected virtual void SetupRegionManager()
        {
            TestContainer.RegisterInstance(RegionManagerMock.Object);
        }

        protected virtual void SetupNavigationService()
        {
            TestContainer.RegisterInstance(NavigationServiceMock.Object);
        }
    }
}