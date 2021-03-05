using System.Windows;
using zhtv.ViewModels;

namespace zhtv.Views
{
    public partial class MusicWindow : Window
    {
        public MusicWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                new Interface(this);
                new Timer(this, mainWindow);
            };
        }
    }
}
