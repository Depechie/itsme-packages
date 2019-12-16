using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Itsme
{
    public class UrlConfiguration
    {
        [JsonProperty(PropertyName = "scopes")]
        public List<Scope> Scopes { get; set; }

        [JsonProperty(PropertyName = "service_code")]
        public string ServiceCode { get; set; }

        [JsonProperty(PropertyName = "request_uri")]
        public string RequestUri { get; set; }

        [JsonProperty(PropertyName = "redirect_uri")]
        public string RedirectUri { get; set; }

        internal string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Scope {
        email,
        profile,
        address,
        phone
    }
}
