using System.Windows.Controls;
using 翻译小工具.ViewModels;

namespace 翻译小工具.Views
{
    /// <summary>
    /// MemoView.xaml 的交互逻辑
    /// </summary>
    public partial class MemoView : UserControl
    {
        private readonly MemoViewModel _memoViewModel;
        public MemoView()
        {
            InitializeComponent();
            _memoViewModel = new MemoViewModel();
            DataContext = _memoViewModel;
            Loaded += _memoViewModel.LoadMemoData;
        }
    }
}
