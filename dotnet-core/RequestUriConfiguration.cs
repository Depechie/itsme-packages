using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

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
        [EnumMember(Value = "tag:sixdots.be,2016-06:claim_eid")]
        ClaimEid,
        [EnumMember(Value = "tag:sixdots.be,2016-06:claim_city_of_birth")]
        ClaimCityOfBirth,
        [EnumMember(Value = "tag:sixdots.be,2016-06:claim_nationality")]
        ClaimNationality,
        [EnumMember(Value = "tag:sixdots.be,2016-06:claim_device")]
        ClaimDevice,
        [EnumMember(Value = "tag:sixdots.be,2017-05:claim_photo")]
        ClaimPhoto
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AcrValue {
        [EnumMember(Value = "tag:sixdots.be,2016-06:acr_basic")]
        ACRBasic,
        [EnumMember(Value = "tag:sixdots.be,2016-06:acr_advanced")]
        ACRAdvanced,
        [EnumMember(Value = "tag:sixdots.be,2016-06:acr_secured")]
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
