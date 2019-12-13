using System.Runtime.InteropServices;
using System.IO;

namespace Itsme
{
    public class Client
    {
        public Client()
        {
            System.Environment.CurrentDirectory = Directory.GetCurrentDirectory();
        }

        public Client(ItsmeSettings settings) : base()
        {
            Init(settings);
        }

        private void Init(ItsmeSettings settings)
        {
            System.Console.WriteLine(Directory.GetCurrentDirectory());
            var response = Wrapper.Init(settings.ToJson());
            var err = Marshal.PtrToStringAnsi(response);
            if (err == null)
                return;
            var error = Error.FromJson(err);
            throw new ItsmeException(error);
        }

        public string GetAuthenticationURL(UrlConfiguration config)
        {
            var response = Wrapper.GetAuthenticationURL(config.ToJson());
            var url = Marshal.PtrToStringAnsi(response.r0);
            var err = Marshal.PtrToStringAnsi(response.r1);
            if (err == null)
                return url;
            var error = Error.FromJson(err);
            throw new ItsmeException(error);
        }

        public User GetUserDetails(string token)
        {
            var response = Wrapper.GetUserDetails(token);
            var data = Marshal.PtrToStringAnsi(response.r0);
            var err = Marshal.PtrToStringAnsi(response.r1);
            if (err == null)
                return User.FromJson(data);
            var error = Error.FromJson(err);
            throw new ItsmeException(error);
        }

        public string CreateRequestURIPayload(RequestUriConfiguration config)
        {
            var response = Wrapper.CreateRequestURIPayload(config.ToJson());
            var jwt = Marshal.PtrToStringAnsi(response.r0);
            var err = Marshal.PtrToStringAnsi(response.r1);
            if (err == null)
                return jwt;
            var error = Error.FromJson(err);
            throw new ItsmeException(error);
        }
    }
}
