using System.Linq;
using CrossApplication.Core.Net.Common.Container;
using CrossApplication.Core.Net.Common.UnitTest._Container.TestClasses;
using FluentAssertions;
using Ninject;
using Xunit;

namespace CrossApplication.Core.Net.Common.UnitTest._Container._NinjectContainer
{
    public class ResolveAll
    {
        [Fact]
        public void Usage()
        {
            var container = new NinjectContainer(new StandardKernel());
            container.RegisterInstance<IInjectionInterface>(new InjectionClass());
            container.RegisterInstance<IInjectionInterface>(new InjectionClass());

            var injectionObjects = container.ResolveAll<IInjectionInterface>();

            injectionObjects.Count().Should().Be(2);
        }
    }
}