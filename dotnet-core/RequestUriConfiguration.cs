using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Itsme
{
    public class RequestUriConfiguration: UrlConfiguration
    {
        [JsonProperty(PropertyName = "acr_value")]
        public AcrValue AcrValue { get; set; }

        [JsonProperty(PropertyName = "nonce")]
        public string Nonce { get; set; }

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

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AcrValue {
        ACRBasic,
        ACRAdvanced,
        ACRSecured
    }
}
