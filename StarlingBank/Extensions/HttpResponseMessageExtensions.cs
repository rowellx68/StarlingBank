using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StarlingBank.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task<T> DeserialiseContent<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}