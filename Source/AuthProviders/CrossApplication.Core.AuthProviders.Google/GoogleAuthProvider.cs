using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Authorization;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;

namespace CrossApplication.Core.AuthProviders.Google
{
    public class GoogleAuthProvider : IAuthorizationProvider
    {
        public string Name => "Google";
        public string Glyph => "Google";

        public async Task<object> AuthorizeTask(string userToken)
        {
            var credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            credPath = Path.Combine(credPath, ".credentials\\", Assembly.GetExecutingAssembly().GetName().Name);

            return await GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
                {
                    ClientId = "37446941611-raij10a54cg27u24v2fvo1q6glvq52hf.apps.googleusercontent.com",
                    ClientSecret = "WVavKXuDM5VurgWk7FxZwvJF"
                }, new[] {"https://www.googleapis.com/auth/contacts.readonly"}
                , userToken
                , CancellationToken.None
                , new FileDataStore(credPath, true));
        }
    }
}