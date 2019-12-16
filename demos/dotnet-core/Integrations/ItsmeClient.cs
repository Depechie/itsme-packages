using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace dotnet_core_api.Integrations
{
    public interface IItsmeClient
    {
        string GetLoginUrl();
        Itsme.User GetUserDetails(string authorization_code);
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

        public Itsme.User GetUserDetails(string authorization_code)
        {
            return _itsmeClient.GetUserDetails(authorization_code);
        }
    }
}
