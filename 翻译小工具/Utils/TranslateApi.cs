using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using HandyControl.Controls;
using 翻译小工具.Models;


namespace 翻译小工具.Utils;

public class TranslateApi
{
    // 百度api使用
    private readonly string bd_appid = "20210731000902409";
    private readonly string bd_salt = "1337128331";
    private readonly string bd_key = "l0eudFB8VrhghMP9Ts6K";
    private readonly string bd_baseUrl = "http://api.fanyi.baidu.com/api/trans/vip/translate";
    public string bd_From { get; set; } = "en";
    public string bd_To { get; set; } = "zh";

    //  kimichat使用
    private readonly string kimi_key = "sk-FWeFulC6S4MbaUgJx3SrAfs3gTBl1gy7wk5XAuHFyq1YxU2S";


    // deepseek使用
    private readonly string ds_key = "sk-372c540fc5e84d04b2ba1b8743b63e7c";




    private static readonly HttpClient _httpClient = new HttpClient();

    public static TranslateApi GetSingleton { get; } = new TranslateApi();


    public async Task<string> CallTranslator(string q, TranslateEngine engine)
    {
        return engine switch
        {
            TranslateEngine.百度通用翻译 => await BaiduEngine(q),
            TranslateEngine.DeepSeek => await DeepSeekEngine(q),
            TranslateEngine.KimiChat => await KimiChatEngine(q),
            _ => ""
        };
    }

    private async Task<string> BaiduEngine(string q)
    {
        string md5Sign = Md5Helper.GetMd5Hash($"{bd_appid}{q}{bd_salt}{bd_key}");
        var query = WebUtility.UrlEncode(q);
        string url = $"{bd_baseUrl}?q={query}&from={bd_From}&to={bd_To}&appid={bd_appid}&salt={bd_salt}&sign={md5Sign}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // 确保响应成功
            string responseBody = await response.Content.ReadAsStringAsync();
            var translationModel = JsonSerializer.Deserialize<BaiduFanYiResponseModel>(responseBody);
            StringBuilder translationResult = new StringBuilder();
            foreach (var transItemResult in translationModel.trans_result)
            {
                translationResult.Append(transItemResult.dst);
            }

            return translationResult.ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return string.Empty;
        }
    }

    private async Task<string> KimiChatEngine(string q)
    {
        return "";
    }
    private async Task<string> DeepSeekEngine(string q)
    {
        return "";
    }

}
