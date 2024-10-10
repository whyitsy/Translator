using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using 翻译小工具.Utils;

namespace 翻译小工具
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string clipboardString = string.Empty;
        private readonly DispatcherTimer _timer;
        private static TranslateText _translateText = new TranslateText();

        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // 设置定时器间隔为 1 秒
            };
            _timer.Tick += Timer_Tick; // 订阅定时器事件
            _timer.Start(); // 启动定时器
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                if (clipboardString != Clipboard.GetText())
                {
                    clipboardString = Clipboard.GetText();


                    OriginText.Text = clipboardString.Replace(Environment.NewLine, "");
                    // 调用api进行翻译
                    Task.Run(async () =>
                    {
                        var translatedText = await _translateText.CallTranslator(clipboardString);
                        Dispatcher.Invoke(() =>
                        {
                            TranslatedText.Text = translatedText;
                        });
                    });
                }
            }
        }


        private void ShutDownButtonClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void SwitchIsTopWindow(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
        }
    }
}