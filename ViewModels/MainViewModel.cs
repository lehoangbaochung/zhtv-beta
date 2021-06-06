using System.Collections.Generic;
using zhtv.ViewModels;
using zhtv.Models;

namespace zhtv.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        const string SHEET_ID = "1znQOtTDJz0UqDs0uB2MQZV3wN0l_J0TrU44d9chH2SI";
        // tab name
        const string SONG = "song";
        const string SONG_VIDEO = "song_video";
        const string SONG_ARTIST = "song_artist";
        const string ALBUM = "album";
        const string ARTIST = "artist";
        const string INFO = "info";

        // tab range
        const string SONG_RANGE = "A2:F";
        const string SONG_VIDEO_RANGE = "A2:D";
        const string SONG_ARTIST_RANGE = "A2:C";
        const string ALBUM_RANGE = "A2:E";
        const string ARTIST_RANGE = "A2:F";
        const string INFO_RANGE = "A2:B";

        public string StreamID { get; set; }

        public static List<Song> Songs = new List<Song>();
        public static List<User> SongVideos = new List<User>();
        public static List<Property> SongArtists = new List<Property>();
        public static List<Artist> Artists = new List<Artist>();
        public static List<Album> Albums = new List<Album>();
        public static List<string> Infos = new List<string>();

        public MainViewModel()
        {
            var sheet = new SheetViewModel();
            sheet.SetId(SHEET_ID);
        }
    }
}
