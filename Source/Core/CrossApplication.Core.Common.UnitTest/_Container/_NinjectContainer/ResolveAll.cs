using System.Linq;
using CrossApplication.Core.Common.Container;
using CrossApplication.Core.Common.UnitTest._Container.TestClasses;
using FluentAssertions;
using Grace.DependencyInjection;
using Xunit;

namespace CrossApplication.Core.Common.UnitTest._Container._NinjectContainer
{
    public class ResolveAll
    {
        [Fact]
        public void Usage()
        {
            var container = new GraceContainer(new DependencyInjectionContainer());
            container.RegisterInstance<IInjectionInterface>(new InjectionClass());
            container.RegisterInstance<IInjectionInterface>(new InjectionClass());

            var injectionObjects = container.ResolveAll<IInjectionInterface>();

            injectionObjects.Count().Should().Be(2);
        }
    }
}