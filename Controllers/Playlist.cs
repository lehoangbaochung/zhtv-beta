using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using zhtv.Models;
using zhtv.Views;

namespace zhtv.Models
{
    public class Playlist
    {
        public List<Song> Items { get; }

        #region objects
        const int PLAYLIST_MIN_COUNT = 15;
        readonly List<Song> library;
        readonly Random random;
        #endregion

        #region delegates
        public Song GetFirstSong() => Items[0];
        public Song GetPlayingSong() => MainWindow.PlayingSong;
        #endregion

        public Playlist(List<Song> songs)
        {
            random = new Random();
            Items = new List<Song>();
            library = songs;

            FillNextSong();
            MainWindow.PlayingSong = Items[0];
        }

        public void Play(WebBrowser webBrowser)
        {
            MainWindow.PlayingSong = Items[0];
            webBrowser.Navigate("http://youtube.com./watch?v=" + MainWindow.PlayingSong.VideoID);

            RemoveAtFirst();
            FillNextSong();
        }

        public void FillNextSong()
        {
            while (Items.Count < PLAYLIST_MIN_COUNT)
            {
                var song = library[random.Next(0, library.Count - 1)];

                if (Items.Contains(song)) return;
                Items.Add(song);
            }
        }

        public void RemoveAtFirst()
        {
            if (Items.Count == 0) return;

            Items[0].OrderUser.Clear();
            Items.RemoveAt(0);
        }

        public void ReloadPlayingSong(WebBrowser webBrowser)
        {
            webBrowser.Navigate("http://youtube.com./watch?v=" + MainWindow.PlayingSong.VideoID);
        }

        public void Sort(Song song)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (song.OrderUser.Count > Items[i].OrderUser.Count)
                {
                    Items.Insert(i, song);
                    break;
                }
            }
        }

        public void Order(Message orderInfo)
        {
            Song orderSong;
            // mỗi acc chỉ được vote tối đa 3 bài 1 lần
            if (Items.Where(s => s.OrderUser.ContainsKey(orderInfo.UserID)).Count() > 2) return;

            var index = Items.FindIndex(s => s.ID == orderInfo.SongID);

            if (index == -1) // Bài hát chưa được order, thêm vào list
            {
                orderSong = library.Find(s => s.ID == orderInfo.SongID);
            }
            else // nếu ng dùng vote bài đã có trong list
            {
                orderSong = Items[index];
                if (orderSong.OrderUser.ContainsKey(orderInfo.UserID)) return;
            }

            orderSong.OrderUser.Add(orderInfo.UserID, orderInfo.UserName);

            var newSong = orderSong;
            Items.Remove(orderSong);
            Sort(newSong);
        }

        public void Refresh()
        {
            Items.Clear();
            FillNextSong();
        }
    }
}
