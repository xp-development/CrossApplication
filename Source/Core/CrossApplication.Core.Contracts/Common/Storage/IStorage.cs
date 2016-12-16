using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Common.Storage
{
    public interface IStorage
    {
        Task SaveAsync<T>(string key, T value) where T : class;
        Task<T> ReadAsync<T>(string key) where T : class;
    }
}