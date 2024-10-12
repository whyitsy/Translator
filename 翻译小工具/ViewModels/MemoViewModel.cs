using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
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


    public void LoadMemoData(object sender, RoutedEventArgs e)
    {
        MemoList.Clear();

        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "记录.txt");
        if (!File.Exists(filePath))
            File.Create(filePath).Dispose();

        using StreamReader reader = new StreamReader(filePath);
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
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "记录.txt");
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine(MemoText);
        }

        MemoList.Add(MemoText);
        MemoText = "";
    }

}