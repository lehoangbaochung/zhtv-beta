using System.Windows;
using zhtv.Models;

namespace zhtv.Views
{
    public partial class MusicWindow : Window
    {
        public MusicWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                new Screen(this);
                new Timer(this);
            };

            Closing += (s, e) =>
            {
                mainWindow.Show();
            };
        }  
    }
}
