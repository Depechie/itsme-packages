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

        [JsonProperty(PropertyName = "advanced_payment_template")]
        public AdvancedPaymentTemplate AdvancedPaymentTemplate { get; set; }

        [JsonProperty(PropertyName = "free_text_template")]
        public FreeTextTemplate FreeTextTemplate { get; set; }
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

    public class AdvancedPaymentTemplate
    {
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }


        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "iban")]
        public string IBAN { get; set; }
    }

    public class FreeTextTemplate
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
