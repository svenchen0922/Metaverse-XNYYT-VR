using System.Security.Cryptography;
using System.Text;

public class MD5Util
{
    protected readonly static string key = "#<&>ICBCApp[^%]%";

    protected readonly static string iv = "#@&*APPICBC)^%(%";

    public static string Encrypt(string palinData, string salt)
    {

        string secretData = "" + salt[0] + salt[2] + palinData + salt[5] + salt[4];

        MD5 md5 = MD5.Create();
        byte[] buffer = Encoding.Default.GetBytes(secretData);
        byte[] md5buffer = md5.ComputeHash(buffer);
        string str = null;
        foreach (byte b in md5buffer)
        {
            str += b.ToString("x2");
        }
        return str;
    }

}
