using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace dotnet_core_api.Integrations
{
    public interface IItsmeClient
    {
        string GetLoginUrl();
        Itsme.User GetUserDetails(string authorization_code, string state);
        string CreateRequestURIPayload();
    }
    public class ItsmeClient: IItsmeClient
    {
        private Itsme.Client _itsmeClient;
        public ItsmeClient(IConfiguration config)
        {
            var settings = new Itsme.ItsmeSettings();
            settings.ClientId = config.GetValue<string>("ClientID", "not_found");
            settings.PrivateJwkSet = config.GetValue<string>("PrivateJWKSet", "not_found");
            settings.Environment = Itsme.Environment.production;
            _itsmeClient = new Itsme.Client(settings);
        }

        public string GetLoginUrl()
        {
            var urlSettings = new Itsme.UrlConfiguration();
            urlSettings.ServiceCode = "MY_SERVICE_CODE";
            urlSettings.Scopes = new List<Itsme.Scope>(){
                Itsme.Scope.profile,
                Itsme.Scope.email,
                Itsme.Scope.address,
                Itsme.Scope.phone
            };
            urlSettings.RequestUri = "https://example.com:443/production/request_uri";
            urlSettings.RedirectUri = "https://example.com/production/redirect";
            return _itsmeClient.GetAuthenticationURL(urlSettings);
        }

        public Itsme.User GetUserDetails(string authorization_code, string state)
        {
            var data = new Itsme.RedirectData();
            data.Code = authorization_code;
            data.RedirectUri = Base64Decode(state);
            return _itsmeClient.GetUserDetails(data);
        }

        public string CreateRequestURIPayload()
        {
            var requestSettings = new Itsme.RequestUriConfiguration();
            requestSettings.ServiceCode = "MY_SERVICE_CODE";
            requestSettings.Scopes = new List<Itsme.Scope>(){
                Itsme.Scope.profile,
                Itsme.Scope.email,
                Itsme.Scope.address,
                Itsme.Scope.phone
            };
            requestSettings.RequestUri = "https://example.com:443/production/request_uri";
            requestSettings.RedirectUri = "https://example.com/production/redirect";
            requestSettings.AcrValue = Itsme.AcrValue.ACRAdvanced;
            requestSettings.Nonce = "noncesense";
            requestSettings.State = Base64Encode(requestSettings.RedirectUri);
            requestSettings.Claims = new List<Itsme.Claim>(){
                Itsme.Claim.ClaimEid,
                Itsme.Claim.ClaimCityOfBirth
            };
            return _itsmeClient.CreateRequestURIPayload(requestSettings);
        }

        private string Base64Encode(string plainText) {
            if (plainText == null) return "";
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private string Base64Decode(string base64EncodedData) {
            if (base64EncodedData == null) return "";
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
