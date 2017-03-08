using System.Threading.Tasks;

namespace Server.Business.Helpers.Interfaces
{
    public interface IEmbeddedResource
    {
        Task<string> GetContents(string resourceName);
    }
}
