using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace zhtv.ViewModels
{
    class SheetViewModel
    {
        readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        readonly string applicationName = "Zither Harp TV";
        readonly SheetsService sheetsService;
        readonly UserCredential credential;

        string id, range;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Range
        {
            get { return range; }
            set { range = value; }
        }

        public IList<IList<object>> Values
        {
            get => sheetsService.Spreadsheets.Values.Get(id, range).Execute().Values;
        }

        public SheetViewModel()
        {
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credentialPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, 
                    Scopes, "user", CancellationToken.None, new FileDataStore(credentialPath, true)).Result;
            }

            sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            }); 
        }
    }
}
