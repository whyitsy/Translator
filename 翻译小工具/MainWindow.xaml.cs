using HandyControl.Controls;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using 翻译小工具.Models;
using 翻译小工具.Utils;
using MessageBox = HandyControl.Controls.MessageBox;

namespace 翻译小工具
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string clipboardString = string.Empty;
        private string lastTranslatedText = string.Empty;
        private readonly DispatcherTimer _timer;
        private TranslateEngine transEngine = TranslateEngine.百度通用翻译;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += LoadMemoList;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // 设置定时器间隔为 1 秒
            };
            _timer.Tick += PoolingClipboard; // 订阅轮询剪切板定时器事件
            _timer.Start(); // 启动定时器
        }

        private void LoadMemoList(object sender, RoutedEventArgs e)
        {
            using var streamReader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "记录.txt"));
            // 逐行读取文件内容，直到文件末尾
          
            while (streamReader.ReadLine() is { } line)
            {
                ListBoxInRB.Items.Add(line);
            }
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

        private void SaveMemoClick(object sender, RoutedEventArgs e)
        {

            using (StreamWriter writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "记录.txt")))
            {
                writer.WriteLine(MemoText.Text);
                ListBoxInRB.Items.Add(MemoText.Text);
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

        private void ChangeMemoListVisibility(object sender, RoutedEventArgs e)
        {
            MemoList.Visibility = MemoList.Visibility switch
            {
                Visibility.Visible => Visibility.Hidden,
                Visibility.Hidden => Visibility.Visible,
                _ => MemoList.Visibility
            };
        }


        private void DoTranslate(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(async () =>
            {
                lastTranslatedText =
                    await TranslateApi.TranslateApiSingleton.CallTranslator(OriginText.Text, TranslateEngine.百度通用翻译);
                TranslatedText.Text = lastTranslatedText;
            });

        }
    }
}