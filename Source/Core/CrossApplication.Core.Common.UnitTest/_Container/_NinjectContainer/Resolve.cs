using CrossApplication.Core.Common.Container;
using CrossApplication.Core.Common.UnitTest._Container.TestClasses;
using CrossApplication.Core.Contracts.Common.Container;
using FluentAssertions;
using Grace.DependencyInjection;
using Xunit;

namespace CrossApplication.Core.Common.UnitTest._Container._NinjectContainer
{
    public class Resolve
    {
        [Fact]
        public void ShouldResolveDifferentInstances()
        {
            var container = new GraceContainer(new DependencyInjectionContainer());
            container.RegisterType<IInjectionInterface, InjectionClass>();

            var injectionObject = container.Resolve<IInjectionInterface>();
            var injectionObject2 = container.Resolve<IInjectionInterface>();

            injectionObject.Should().NotBeNull();
            injectionObject2.Should().NotBeNull();
            injectionObject.Should().NotBe(injectionObject2);
        }

        [Fact]
        public void ShouldResolveSameInstance()
        {
            var container = new GraceContainer(new DependencyInjectionContainer());
            container.RegisterType<IInjectionInterface, InjectionClass>(Lifetime.PerContainer);

            var injectionObject = container.Resolve<IInjectionInterface>();
            var injectionObject2 = container.Resolve<IInjectionInterface>();

            injectionObject.Should().NotBeNull();
            injectionObject2.Should().NotBeNull();
            injectionObject.Should().Be(injectionObject2);
        }

        [Fact]
        public void ShouldResolveSameInstanceIfInstanceIsRegistered()
        {
            var container = new GraceContainer(new DependencyInjectionContainer());
            container.RegisterInstance<IInjectionInterface>(new InjectionClass());

            var injectionObject = container.Resolve<IInjectionInterface>();
            var injectionObject2 = container.Resolve<IInjectionInterface>();

            injectionObject.Should().NotBeNull();
            injectionObject2.Should().NotBeNull();
            injectionObject.Should().Be(injectionObject2);
        }

        [Fact]
        public void Usage()
        {
            var container = new GraceContainer(new DependencyInjectionContainer());
            container.RegisterType<IInjectionInterface, InjectionClass>();

            var injectionObject = container.Resolve<IInjectionInterface>();

            injectionObject.Should().NotBeNull();
        }
    }
}