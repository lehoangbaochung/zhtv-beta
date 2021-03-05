using System;
using System.Linq;
using System.Windows.Media.Imaging;
using zhtv.Views;

namespace zhtv.ViewModels
{
    class Interface
    {
        public Interface(MusicWindow musicWindow)
        {
            // song name
            musicWindow.txtSong1.Text = SongName(0);
            musicWindow.txtSong2.Text = SongName(1);
            musicWindow.txtSong3.Text = SongName(2);
            // song order count
            musicWindow.prbSong1.Value = SongOrderCount(0);
            musicWindow.prbSong2.Value = SongOrderCount(0);
            musicWindow.prbSong3.Value = SongOrderCount(0);
            // other 
            musicWindow.txtPlaylist.Text = Playlist();
            // screen image
            musicWindow.imgScreen.Source = ArtistImage();
        }

        public static string SongID(int index) 
            => $"{ Player.Playlist[index].ID }";

        public static string SongName(int index) 
            => $"{ Player.Playlist[index].ID }: { Player.Playlist[index].Name }";

        public static int SongOrderCount(int index) 
            => Player.Playlist[index].OrderUser.Count * 10 + 10;

        public static string Playlist()
        {
            string playlist = "";

            for (int i = 0; i < 15; i++)
            {
                playlist += SongName(i) + " ";
            }

            return playlist;
        }

        public static string PlayingSongName()
        {
            string orderUser = $"Khán giả yêu cầu cuối cùng: { Player.Playlist[0].OrderUser.ElementAt(Player.Playlist[0].OrderUser.Count - 1).Value }\n";

            if (Player.Playlist[0].OrderUser.Count == 0)
                return $"Đang phát: { Player.Playlist[0].Name }\nThể hiện: { Player.Playlist[0].Artist }\nMã số: { Player.Playlist[0].ID }";
            else
                return $"{ orderUser }\nĐang phát: { Player.Playlist[0].Name }\nThể hiện: { Player.Playlist[0].Artist }\nMã số: { Player.Playlist[0].ID }";
        }

        public static string SyntaxOrder()
        {
            var rd = new Random(); 
            
            return "Soạn tin: ZM " + Player.Library[rd.Next(1, Player.Library.Count)].ID + " gửi 6" + rd.Next(1, 7) + "77";
        }

        public static BitmapImage ArtistImage()
        {
            var bi = new BitmapImage();

            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;

            try
            {
                bi.UriSource = new Uri(Player.PlayingSong.AlbumID);
                bi.EndInit();
            }
            catch (Exception)
            {
                bi.UriSource = null;
                bi.EndInit();
            }

            return bi;
        }
    }
}
