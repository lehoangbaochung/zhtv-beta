using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using zhtv.Views;

namespace zhtv.Models
{
    class Timer
    {
        readonly Random random = new Random();
        readonly TextBlock tbInfo, tbOrder, tbPlaylist, tbPlayingSong;
        DispatcherTimer timer;
        
        public Timer(MainWindow mainWindow)
        {
            Player(mainWindow.webPlayer).Start();
        }

        public Timer(MusicWindow musicWindow)
        {
            tbInfo = musicWindow.txtInfo;
            tbOrder = musicWindow.txtOrder;
            tbPlaylist = musicWindow.txtPlaylist;
            tbPlayingSong = musicWindow.txtNextSong;

            Info(1150, -3000).Start();
            Playlist(1200, -4000).Start();
            PlayingSongName().Start();
            OrderInfo().Start();
            SongBar(musicWindow).Start();
        }

        DispatcherTimer SongBar(MusicWindow musicWindow)
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

            timer.Tick += (s, e) =>
            {
                new Screen(musicWindow);
            };

            return timer;
        }

        DispatcherTimer Player(WebBrowser webBrowser)
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            decimal tick = 0;

            timer.Tick += (s, e) =>
            {
                tick += 1;

                if (tick != MainWindow.PlayingSong.Duration) return;

                tick = 0;
                MainWindow.Playlist.Play(webBrowser);
            };

            return timer;
        }

        DispatcherTimer OrderInfo()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.5) };

            timer.Tick += (s, e) =>
            {
                tbOrder.Text = Screen.SyntaxOrder();
            };

            return timer;
        }

        DispatcherTimer Info(double startPoint, double endPoint)
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.001) };

            timer.Tick += (s, e) =>
            {
                var left = tbInfo.Margin.Left;

                left -= 2;
                tbInfo.Margin = new Thickness(left, tbInfo.Margin.Top, tbInfo.Margin.Right, tbInfo.Margin.Bottom);

                if (left != endPoint) return;

                tbInfo.Text = MainWindow.Information[random.Next(0, MainWindow.Information.Count)];
                tbInfo.Margin = new Thickness(startPoint, tbInfo.Margin.Top, tbInfo.Margin.Right, tbInfo.Margin.Bottom);              
            };

            return timer;
        }

        DispatcherTimer Playlist(double startPoint, double endPoint)
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.001) };

            timer.Tick += (s, e) =>
            {
                var left = tbPlaylist.Margin.Left;

                left -= 2;
                tbPlaylist.Margin = new Thickness(left, tbPlaylist.Margin.Top, tbPlaylist.Margin.Right, tbPlaylist.Margin.Bottom);

                if (left != endPoint) return;

                tbPlaylist.Margin = new Thickness(startPoint, tbPlaylist.Margin.Top, tbPlaylist.Margin.Right, tbPlaylist.Margin.Bottom);
            };

            return timer;
        }

        DispatcherTimer PlayingSongName()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            decimal tick = 0;

            timer.Tick += (s, e) =>
            {
                tick += 1;

                if (Math.Truncate(tick / 10) % 5 == 0)
                {
                    tbPlayingSong.Background = Brushes.White;
                    tbPlayingSong.Foreground = Brushes.Black;
                }
                else
                {
                    tbPlayingSong.Background = Brushes.Transparent;
                    tbPlayingSong.Foreground = Brushes.Transparent;
                }                
            };

            return timer;
        }
    }
}
