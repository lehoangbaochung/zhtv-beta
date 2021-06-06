using System;
using System.Linq;
using System.Windows.Media.Imaging;
using zhtv.Views;

namespace zhtv.Models
{
    /// <summary>
    /// Displays screen on the top of window
    /// </summary>
    class Screen
    {
        static readonly Random random = new Random();

        public Screen(MusicWindow musicWindow)
        {
            musicWindow.txtSong1.Text = SongName(0);
            musicWindow.txtSong2.Text = SongName(1);
            musicWindow.txtSong3.Text = SongName(2);

            musicWindow.prbSong1.Value = SongOrderCount(0);
            musicWindow.prbSong2.Value = SongOrderCount(1);
            musicWindow.prbSong3.Value = SongOrderCount(2);

            musicWindow.imgScreen.Source = AlbumImage();
            musicWindow.txtPlaylist.Text = Playlist();
            musicWindow.txtNextSong.Text = PlayingSongName();
        }

        string SongId(int index) 
            => $"{ MainWindow.Playlist.Items[index].ID }";

        string SongName(int index) 
            => $"{ MainWindow.Playlist.Items[index].ID }: { MainWindow.Playlist.Items[index].Name }";

        int SongOrderCount(int index) 
            => MainWindow.Playlist.Items[index].OrderUser.Count * 10 + 10;

        string Playlist()
        {
            string playlist = "";

            for (int i = 0; i < 15; i++) 
            { 
                playlist += SongName(i) + " "; 
            };

            return playlist;
        }

        string PlayingSongName()
        {
            if (MainWindow.PlayingSong.OrderUser.Count > 0)
            {
                string orderUser = $"Khán giả yêu cầu cuối cùng: { MainWindow.PlayingSong.OrderUser.ElementAt(MainWindow.PlayingSong.OrderUser.Count - 1).Value }\n";
                return $"{ orderUser }\nĐang phát: { MainWindow.PlayingSong.Name }\nThể hiện: { MainWindow.PlayingSong.Artist }\nMã số: { MainWindow.PlayingSong.ID }";
            }    
            else
                return $"Đang phát: { MainWindow.PlayingSong.Name }\nThể hiện: { MainWindow.PlayingSong.Artist }\nMã số: { MainWindow.PlayingSong.ID }";
        }

        public static string SyntaxOrder()
            => "Soạn tin: ZM " + MainWindow.Library[random.Next(1, MainWindow.Library.Count)].ID + " gửi 6" + random.Next(1, 7) + "77";

        BitmapImage AlbumImage()
        {
            var bi = new BitmapImage();

            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;

            try
            {
                bi.UriSource = new Uri(MainWindow.PlayingSong.AlbumID);
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
