using System.Windows;
using System.Windows.Input;

namespace 翻译小工具
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowDragMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ShutDownApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void SwitchIsTop(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
        }

    }
}