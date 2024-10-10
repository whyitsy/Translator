using System.Security.Cryptography;
using System.Text;

namespace 翻译小工具.Utils;

public class Md5Helper
{
    private static readonly MD5 Md5 = MD5.Create();


    public static string GetMd5Hash(string input)
    {
        byte[] data = Md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        Span<char> result = stackalloc char[data.Length * 2];
        for (int i = 0; i < data.Length; i++)
        {
            data[i].TryFormat(result.Slice(i * 2, 2), out _, "x2");
        }
        return new string(result);
    }

}