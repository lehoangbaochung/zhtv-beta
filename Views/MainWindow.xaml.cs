using System;
using System.Windows;
using System.Windows.Controls;
using zhtv.ViewModels;

namespace zhtv.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Load();
            Player.FillNextSong();
            new Player(webPlayer);

            switch ((sender as Button).Name)
            {
                case "btnStart":
                    new MusicWindow(this).Show();
                    break;
            }

            Hide();           
        }

        private void Load()
        {
            var songs = new Sheet().Get(tbxSheetID.Text, tbxSongSheet.Text, tbxSheetRange.Text);
            var infos = new Sheet().Get(tbxSheetID.Text, tbxInfoSheet.Text, tbxInfoSheetRange.Text);

            if (songs == null && songs.Count < 6) return;

            foreach (var song in songs)
            {
                Player.Library.Add(new Song
                {
                    ID = Convert.ToInt32(song[0]),
                    Name = song[1].ToString(),
                    Artist = song[2].ToString(),
                    Duration = Convert.ToInt32(song[3]),
                    VideoID = song[4].ToString(),
                    AlbumID = song[5].ToString()
                });
            }

            foreach (var info in infos)
            {
                Player.Info.Add(info[1].ToString());
            }
        }
    }
}
