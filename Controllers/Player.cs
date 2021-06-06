using System.Collections.Generic;
using zhtv.Views;

namespace zhtv.Models
{
    class Player 
    {
        readonly MainWindow mainWindow;
        public static Playlist Playlist;
        public static List<Song> Library;
        public static List<string> Information;

        public Player(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void Prepare()
        {
            var sheetId = mainWindow.tbxSheetID.Text;
            var songSheet = new string[] { sheetId, mainWindow.tbxSongSheet.Text, mainWindow.tbxSheetRange.Text };
            var infoSheet = new string[] { sheetId, mainWindow.tbxInfoSheet.Text, mainWindow.tbxInfoSheetRange.Text };

            Library = new Sheet(songSheet[0], songSheet[1], songSheet[2]).GetSongs();
            Information = new Sheet(infoSheet[0], infoSheet[1], infoSheet[2]).GetInfos();
        }

        public void Start()
        {
            Playlist = new Playlist(Library);
        }
    }    
}
