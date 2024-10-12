using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
