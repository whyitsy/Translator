using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using 翻译小工具.Models;
using 翻译小工具.Utils;

namespace 翻译小工具
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string clipboardString = string.Empty;
        private string lastTranslatedText = string.Empty;
        private readonly DispatcherTimer _timer;
        private TranslateEngine transEngine = TranslateEngine.百度通用翻译;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += RotateImageAnimation;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // 设置定时器间隔为 1 秒
            };
            _timer.Tick += PoolingClipboard; // 订阅轮询剪切板定时器事件
            _timer.Start(); // 启动定时器


        }

        private void PoolingClipboard(object? sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                if (clipboardString != Clipboard.GetText() && lastTranslatedText != Clipboard.GetText())
                {
                    clipboardString = Clipboard.GetText();

                    OriginText.Text = clipboardString.Replace(Environment.NewLine, "");
                    // 调用api进行翻译
                    Task.Run(async () =>
                    {
                        lastTranslatedText = await TranslateApi.TranslateApiSingleton.CallTranslator(clipboardString, transEngine);
                        Dispatcher.Invoke(() =>
                        {
                            TranslatedText.Text = lastTranslatedText;
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

        private void CopyTranslatedTextButtonClicked(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TranslatedText.Text);
        }

        private void RotateImageAnimation(object sender, RoutedEventArgs e)
        {
            Storyboard rotationStoryboard = new Storyboard();
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(10)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            Storyboard.SetTargetName(doubleAnimation, "RotateTransform");
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(RotateTransform.AngleProperty));
            rotationStoryboard.Children.Add(doubleAnimation);

            rotationStoryboard.Begin(this);
        }

        private void SaveMemoClick(object sender, RoutedEventArgs e)
        {
            string filePath = "记录.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(MemoText.Text);
            }
            MemoText.Text = "";
        }

        private void OnBaiduEngineSelected(object sender, RoutedEventArgs e)
        {
            transEngine = TranslateEngine.百度通用翻译;
        }

        private void OnKimiEngineSelected(object sender, RoutedEventArgs e)
        {
            transEngine = TranslateEngine.百度通用翻译;
        }

        private void OnDeepSeekEngineSelected(object sender, RoutedEventArgs e)
        {
            transEngine = TranslateEngine.百度通用翻译;
        }
    }
}