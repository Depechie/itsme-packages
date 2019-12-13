using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Itsme
{
    public class RequestUriConfiguration: UrlConfiguration
    {
        [JsonProperty(PropertyName = "acr_value")]
        public string ACRValue { get; set; }

        [JsonProperty(PropertyName = "nonce")]
        public string None { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "claims")]
        public List<Claim> Claims { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Claim {
        ClaimEid,
        ClaimCityOfBirth,
        ClaimNationality,
        ClaimDevice,
        ClaimPhoto
    }
}
