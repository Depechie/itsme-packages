using Newtonsoft.Json;
using System;

namespace Itsme
{
    public class User
    {
        [JsonProperty(PropertyName = "sub")]
        public string Sub { get; set; }

        [JsonProperty(PropertyName = "aud")]
        public string Aud { get; set; }

        [JsonProperty(PropertyName = "eid")]
        public Eid Eid { get; set; }

        [JsonProperty(PropertyName = "birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty(PropertyName = "city_of_birth")]
        public string CityOfBirth { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "iss")]
        public string Iss { get; set; }

        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "phone_number_verified")]
        public bool PhoneNumberVerified { get; set; }

        [JsonProperty(PropertyName = "given_name")]
        public string GivenName { get; set; }

        [JsonProperty(PropertyName = "family_name")]
        public string FamilyName { get; set; }

        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "email_verified")]
        public bool EmailVerified { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address ParsedAddress { get; set; }

        [JsonProperty(PropertyName = "nationality")]
        public string Nationality { get; set; }

        [JsonProperty(PropertyName = "photo")]
        public string Photo { get; set; }

        [JsonProperty(PropertyName = "device")]
        public Device Device { get; set; }

        internal static User FromJson(string json)
        {
            return JsonConvert.DeserializeObject<User>(json);
        }
    }

    public class Eid
    {
        [JsonProperty(PropertyName = "issuance_locality")]
        public string IssuanceLocality { get; set; }

        [JsonProperty(PropertyName = "national_number")]
        public string NationalNumber { get; set; }

        [JsonProperty(PropertyName = "eid")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "validity_to")]
        public DateTime? ValidityTo { get; set; }

        [JsonProperty(PropertyName = "validity_from")]
        public DateTime? ValidityFrom { get; set; }

        [JsonProperty(PropertyName = "read_date")]
        public DateTime? ReadDate { get; set; }
    }

    public class Address
    {
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "street_address")]
        public string StreetAddress { get; set; }

        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set; }

        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }
    }

    public class Device
    {
        [JsonProperty(PropertyName = "os")]
        public string OS { get; set; }

        [JsonProperty(PropertyName = "appName")]
        public string AppName { get; set; }

        [JsonProperty(PropertyName = "appRelease")]
        public string AppRelease { get; set; }

        [JsonProperty(PropertyName = "deviceLabel")]
        public string DeviceLabel { get; set; }

        [JsonProperty(PropertyName = "debugEnabled")]
        public bool DebugEnabled { get; set; }

        [JsonProperty(PropertyName = "deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty(PropertyName = "osRelease")]
        public string OSRelease { get; set; }

        [JsonProperty(PropertyName = "manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty(PropertyName = "hasSimEnabled")]
        public bool HasSimEnabled { get; set; }

        [JsonProperty(PropertyName = "deviceLockLevel")]
        public string DeviceLockLevel { get; set; }

        [JsonProperty(PropertyName = "smsEnabled")]
        public bool SMSEnabled { get; set; }

        [JsonProperty(PropertyName = "rooted")]
        public bool Rooted { get; set; }

        [JsonProperty(PropertyName = "deviceModel")]
        public string DeviceModel { get; set; }

        [JsonProperty(PropertyName = "msisdn")]
        public string MSISDN { get; set; }

        [JsonProperty(PropertyName = "sdkRelease")]
        public string SDKRelease { get; set; }
    }
}
