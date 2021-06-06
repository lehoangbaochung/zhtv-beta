using System.Windows;

namespace zhtv.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.MainViewModel
            {
                StreamID = tbxVideoID.Text
            }; 
        }      
    }
}
