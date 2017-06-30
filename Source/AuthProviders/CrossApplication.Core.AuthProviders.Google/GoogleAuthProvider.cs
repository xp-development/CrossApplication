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
            return await GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
                {
                    ClientId = "37446941611-ncr5oud7kmcenrl27f18am3bad6mk6fl.apps.googleusercontent.com",
                    ClientSecret = "ph2noMSorT2aXhHWCjeKISYS"
                }, new[] { "https://www.googleapis.com/auth/contacts.readonly" }
                , userToken
                , CancellationToken.None
                , new FileDataStore("Google"));

//            var parameters = new OAuth2Parameters();
//            parameters.AccessToken = credential.Token.AccessToken;
//            parameters.RefreshToken = credential.Token.RefreshToken;
//
//
//            var settings = new RequestSettings("CrossApplicationContacts", parameters);
//            var cr = new ContactsRequest(settings);
//            var f = cr.GetContacts();
//            foreach (var c in f.Entries)
//                Console.WriteLine(c.Name.FullName);
//            IsAuthorized = true;
//            return IsAuthorized;
        }
    }
}
