using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using 翻译小工具.Utils;
using 翻译小工具.ViewModels;

namespace 翻译小工具.Views
{
    /// <summary>
    /// TranslatorLayoutView.xaml 的交互逻辑
    /// </summary>
    public partial class TranslatorLayoutView : UserControl
    {
        private readonly TranslatorLayoutViewModel _translatorViewModel;
        private readonly DispatcherTimer _timer;
        public TranslatorLayoutView()
        {
            InitializeComponent();
            _translatorViewModel = new TranslatorLayoutViewModel();
            DataContext = _translatorViewModel;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += PoolingClipBoard;
            _timer.Start();
        }

        /// <summary>
        /// 注册_timer.Tick的事件，轮询剪切板进行翻译
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PoolingClipBoard(object? sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                // 剪切板内容变化后，且不是上次翻译的结果就进行翻译
                if (_translatorViewModel.TextToTranslate != Clipboard.GetText() &
                    _translatorViewModel.TranslatedText != Clipboard.GetText())
                {
                    _translatorViewModel.TextToTranslate = Clipboard.GetText();

                    Dispatcher.Invoke(async () =>
                    {
                        _translatorViewModel.TranslatedText = await TranslateApi.GetSingleton.CallTranslator(_translatorViewModel.TextToTranslate,
                         _translatorViewModel.TranslateEngine);
                    });
                }
            }

        }
    }
}
