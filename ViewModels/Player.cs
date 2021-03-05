using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using zhtv.Models;

namespace zhtv.ViewModels
{
    class Player 
    {
        #region private objects
        static readonly Random rd = new Random();
        const int PLAYLIST_MIN_COUNT = 15;
        #endregion

        #region public objects
        public static Song PlayingSong;
        public static List<Song> Playlist = new List<Song>();
        public static List<Song> Library = new List<Song>();
        public static List<string> Info = new List<string>();
        #endregion

        public Player(WebBrowser webBrowser)
        {
            PlayingSong = Playlist[0];
            webBrowser.Navigate("http://youtube.com./watch?v=" + PlayingSong.VideoID);

            FillNextSong();
        }

        public static void FillNextSong()
        {
            if (Playlist.Count != 0)
            {
                Playlist[0].OrderUser.Clear();
                Playlist.RemoveAt(0);
            }

            while (Playlist.Count < PLAYLIST_MIN_COUNT)
            {
                var song = Library[rd.Next(0, Library.Count - 1)];

                if (!Playlist.Contains(song)) 
                    Playlist.Add(song);
            }
        }

        public static void OrderSong(OrderInfo order)
        {
            // mỗi acc chỉ được vote tối đa 3 bài 1 lần
            if (Playlist.Where(s => s.OrderUser.ContainsKey(order.UserID)).Count() > 3) return;

            var index = Playlist.FindIndex(s => s.ID == order.SongID);

            if (index == -1) // Bài hát chưa được order, thêm vào list
            {
                var song = Library.Find(s => s.ID == order.SongID);

                Sort(song, order);
            }
            else // nếu ng dùng vote bài đã có trong list
            {
                var song = Playlist[index];

                if (!song.OrderUser.ContainsKey(order.UserID)) Sort(song, order);
            }
        }

        private static void Sort(Song song, OrderInfo order)
        {
            song.OrderUser.Add(order.UserID, order.UserName);

            var newSong = song;
            Playlist.Remove(song);

            for (int i = 0; i < Playlist.Count; i++)
            {
                if (newSong.OrderUser.Count > Playlist[i].OrderUser.Count)
                {
                    Playlist.Insert(i, newSong);
                    break;
                }
            }
        }
    }
}
