using Server.Business.Helpers.Interfaces;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Server.Business.Helpers
{
    public class EmbeddedResource : IEmbeddedResource
    {
        public async Task<string> GetContents(string resourceName)
        {
            var result = string.Empty;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        result = await reader.ReadToEndAsync();
                    }
                }
            }

            return result;
        }
    }
}
