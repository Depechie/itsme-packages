using Microsoft.Extensions.Configuration;

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
            settings.RedirectUri = config.GetValue<string>("RedirectURI", "not_found");
            settings.PrivateJwkSet = config.GetValue<string>("PrivateJWKSet", "not_found");
            settings.Environment = config.GetValue<string>("AppEnvironment", "not_found");
            _itsmeClient = new Itsme.Client(settings);
        }

        public string GetLoginUrl()
        {
            var urlSettings = new Itsme.UrlConfiguration();
            urlSettings.ServiceCode = "MY_SERVICE_CODE";
            urlSettings.Scopes = new string[]{"profile", "email", "address", "phone", "eid"};
            return _itsmeClient.GetAuthenticationURL(urlSettings);
        }

        public Itsme.User GetUserDetails(string authorization_code)
        {
            return _itsmeClient.GetUserDetails(authorization_code);
        }
    }
}
