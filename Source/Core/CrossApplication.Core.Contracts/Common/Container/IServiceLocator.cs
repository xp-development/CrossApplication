namespace CrossApplication.Core.Contracts.Common.Container
{
    public interface IServiceLocator
    {
        T GetInstance<T>();
        T GetInstance<T>(string name);
    }
}