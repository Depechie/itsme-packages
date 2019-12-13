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

        [JsonProperty(PropertyName = "app_environment")]
        public Environment Environment { get; set; }

        internal string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public const string Production = "production";
        public const string Sandbox = "e2e";
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Environment {
        production,
        e2e
    }
}
