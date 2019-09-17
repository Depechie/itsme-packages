using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Itsme
{
    public class ItsmeSettings
    {
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty(PropertyName = "private_jwk_set")]
        public string PrivateJwkSet { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "app_environment")]
        public ItsmeEnvironment Environment { get; set; }

        internal string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public enum ItsmeEnvironment
    {
        production,
        e2e
    }
}
