using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using 翻译小工具.Models;
using 翻译小工具.Utils;

namespace 翻译小工具
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string clipboardString = string.Empty;
        private string lastTranslatedText = string.Empty;
        private TranslateEngine transEngine = TranslateEngine.百度通用翻译;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += LoadMemoList;
            Loaded += LoadMemoList;
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


    }
}