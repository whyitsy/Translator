using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using 翻译小工具.Models;
using 翻译小工具.Utils;

namespace 翻译小工具.ViewModels;

public partial class TranslatorLayoutViewModel:ObservableObject
{
    public TranslateEngine TranslateEngine { get; set; } = TranslateEngine.百度通用翻译;

    [ObservableProperty]
    private string _textToTranslate = string.Empty;

    [ObservableProperty]
    private string _translatedText = string.Empty;


    [RelayCommand]  
    private void CopyOriginToClipBoard(string textToClipboard)
    {
        Clipboard.SetText(textToClipboard);
    }

    [RelayCommand]
    private void CopyResultToClipboard()
    {
        Clipboard.SetText(TranslatedText);
    }


}