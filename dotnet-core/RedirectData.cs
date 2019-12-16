using Newtonsoft.Json;

namespace Itsme
{
    public class RedirectData
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "redirect_uri")]
        public string RedirectUri { get; set; }

        internal string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
