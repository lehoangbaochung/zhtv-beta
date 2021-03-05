using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using zhtv.Views;

namespace zhtv.ViewModels
{
    class Timer
    {
        public Timer(MusicWindow musicWindow, MainWindow mainWindow)
        {
            Info(musicWindow.txtInfo, 1150, -3000, 0.001);
            Playlist(musicWindow.txtPlaylist, 1200, -4000, 0.001);
            PlayingSongName(musicWindow.txtNextSong, musicWindow.txtOrder, 1);

            var playTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            var orderTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(20) };
            decimal tick = 0;

            playTimer.Tick += (s, e) =>
            {
                tick += 1;

                if (tick == Player.PlayingSong.Duration)
                {
                    new Interface(musicWindow);
                    new Player(mainWindow.webPlayer);

                    tick = 0;
                }
            };

            orderTimer.Tick += (s, e) =>
            {
                try
                {
                    Stream.Order(mainWindow.tbxVideoID.Text);
                    new Interface(musicWindow);
                }
                catch (Exception) { }
            };

            playTimer.Start();
            orderTimer.Start();
        }

        private static void Info(TextBlock textBlock, double startPoint, double endPoint, double span)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(span) };
            var rd = new Random();

            timer.Tick += (s, e) =>
            {
                var left = textBlock.Margin.Left;

                left -= 2;
                textBlock.Margin = new Thickness(left, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);

                if (left != endPoint) return;

                textBlock.Text = Player.Info[rd.Next(0, Player.Info.Count)];
                textBlock.Margin = new Thickness(startPoint, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);              
            };

            timer.Start();
        }

        private static void Playlist(TextBlock textBlock, double startPoint, double endPoint, double span)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(span) };

            timer.Tick += (s, e) =>
            {
                var left = textBlock.Margin.Left;

                left -= 2;
                textBlock.Margin = new Thickness(left, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);

                if (left != endPoint) return;

                textBlock.Margin = new Thickness(startPoint, textBlock.Margin.Top, textBlock.Margin.Right, textBlock.Margin.Bottom);
            };

            timer.Start();
        }

        private static void PlayingSongName(TextBlock textBlock, TextBlock txtOrder, double span)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(span) };
            decimal tick = 0;

            timer.Tick += (s, e) =>
            {
                tick += 1;
                txtOrder.Text = Interface.SyntaxOrder();

                if (Math.Truncate(tick / 10) % 5 == 0)
                {
                    textBlock.Background = Brushes.White;
                    textBlock.Foreground = Brushes.Black;
                }
                else
                {
                    textBlock.Background = Brushes.Transparent;
                    textBlock.Foreground = Brushes.Transparent;
                }                
            };

            timer.Start();
        }
    }
}
