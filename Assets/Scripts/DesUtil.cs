using System;
using System.Security.Cryptography;
using System.Text;

public class DesUtil
{
    public static string Encrypt(string content, string aesKey, string aesIV)
    {

        byte[] byteKEY = Encoding.UTF8.GetBytes(aesKey);
        byte[] byteIV = Encoding.UTF8.GetBytes(aesIV);

        byte[] byteContnet = Encoding.UTF8.GetBytes(content);

        var _aes = new RijndaelManaged
        {
            Padding = PaddingMode.PKCS7,
            Mode = CipherMode.CBC,
            Key = byteKEY,
            IV = byteIV
        };

        var _crypto = _aes.CreateEncryptor(byteKEY, byteIV);
        byte[] decrypted = _crypto.TransformFinalBlock(byteContnet, 0, byteContnet.Length);

        _crypto.Dispose();

        return Convert.ToBase64String(decrypted);
    }
}
