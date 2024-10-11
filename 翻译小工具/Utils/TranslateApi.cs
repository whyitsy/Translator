using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using HandyControl.Controls;


namespace 翻译小工具.Utils;

public class TranslateApi
{
    private readonly string appid = "20210731000902409";
    private readonly string salt = "1337128331";
    private readonly string key = "l0eudFB8VrhghMP9Ts6K";

    private string query = string.Empty;

    private readonly string baseUrl = "http://api.fanyi.baidu.com/api/trans/vip/translate";

    public string From { get; set; } = "en";
    public string To { get; set; } = "zh";

    private static readonly HttpClient _httpClient = new HttpClient();

    public static TranslateApi TranslateApiSingleton { get; } = new TranslateApi();


    public async Task<string> CallTranslator(string q)
    {
        
        string md5Sign = Md5Helper.GetMd5Hash($"{appid}{q}{salt}{key}");
        query = WebUtility.UrlEncode(q);
        string url = $"{baseUrl}?q={query}&from={From}&to={To}&appid={appid}&salt={salt}&sign={md5Sign}";

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

}
