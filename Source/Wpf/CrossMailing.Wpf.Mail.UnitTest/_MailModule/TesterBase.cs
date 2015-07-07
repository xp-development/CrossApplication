using CrossMailing.Wpf.Common;
using CrossMailing.Wpf.Common.Events;
using Microsoft.Practices.Unity;
using Moq;
using Prism.Events;
using Prism.Regions;

namespace CrossMailing.Wpf.Mail.UnitTest._MailModule
{
    public abstract class TesterBase
    {
        protected UnityContainer TestContainer;
        protected Mock<IEventAggregator> EventAggregatorMock;
        protected Mock<IUnityContainer> UnityContainerMock;
        protected MockRegionManager RegionManagerMock;

        protected TesterBase()
        {
            TestContainer = new UnityContainer();

            EventAggregatorMock = new Mock<IEventAggregator>();
            UnityContainerMock = new Mock<IUnityContainer>();
            RegionManagerMock = new MockRegionManager();

            SetupEventAggregator();
            SetupUnityContainer();
            SetupRegionManager();
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
            RegionManagerMock.Regions.Add(RegionNames.MainRegion, new MockRegion());
            TestContainer.RegisterInstance<IRegionManager>(RegionManagerMock);
        }
    }
}