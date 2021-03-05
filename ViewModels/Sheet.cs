using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace zhtv.ViewModels
{
    class Sheet
    {
        private static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private static readonly string ApplicationName = "Zither Harp TV";
        private static SheetsService sheetsService;

        public Sheet()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user",
                    CancellationToken.None, new FileDataStore(credPath, true)).Result;
            }

            sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
        }

        public IList<IList<object>> Get(string id, string tabName, string range)
        {
            if (tabName == "" || tabName == null)
                return sheetsService.Spreadsheets.Values.Get(id, range).Execute().Values;
            else
                return sheetsService.Spreadsheets.Values.Get(id, tabName + "!" + range).Execute().Values;
        }
    }
}
