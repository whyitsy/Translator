using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace 翻译小工具.ViewModels;

public partial class MemoViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _memoVisibility = true;

    [ObservableProperty]
    private string _memoText = string.Empty;

    public ObservableCollection<string> MemoList { get; set; } = new ObservableCollection<string>();

    public MemoViewModel()
    {
        LoadMemoData();
    }

    private void LoadMemoData()
    {
        MemoList.Clear();
        using StreamReader reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "记录.txt"));
        while (reader.ReadLine() is { } line)
        {
            MemoList.Add(line);
        }
    }

    [RelayCommand]
    private void ChangeMemoBoxVisibility()
    {
        MemoVisibility = !MemoVisibility;
    }

    [RelayCommand]
    private void AddMemoItem()
    {
        using (StreamWriter writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "记录.txt"), append: true))
        {
            writer.WriteLine(MemoText);
        }

        MemoList.Add(MemoText);
        MemoText = "";
    }

}