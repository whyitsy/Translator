namespace 翻译小工具.Utils;

public class BaiduFanYiResponseModel
{
    public string from { get; set; }
    public string to { get; set; }
    public TransItemResult[] trans_result { get; set; }
}
public class TransItemResult
{
    public string src { get; set; }
    public string dst { get; set; }
}