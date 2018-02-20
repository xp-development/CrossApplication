using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Settings
{
    public interface ISettingsChild
    {
        Task SaveAsync();
    }
}