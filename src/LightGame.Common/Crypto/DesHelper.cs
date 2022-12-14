using System.Security.Cryptography;
using System.Text;

namespace LightGame.Common;

/// <summary>
/// DES Helper
/// </summary>
public static class DesHelper
{
    public static Encoding Encoding { get; set; } = Encoding.UTF8;

    public static byte[] GenerateKey()
    {
        using var des = DES.Create();
        des.GenerateKey();
        return des.Key;
    }

    public static byte[] GenerateVector()
    {
        using var des = DES.Create();
        des.GenerateIV();
        return des.IV;
    }

    public static (byte[] key, byte[] vector) GenerateKeyAndVector()
    {
        using var des = DES.Create();
        des.GenerateKey();
        des.GenerateIV();
        return (des.Key, des.IV);
    }

    /// <summary>
    /// DES Encrypt
    /// </summary>
    /// <param name="original"></param>
    /// <param name="key"></param>
    /// <param name="vector"></param>
    /// <param name="cipherMode"></param>
    /// <param name="paddingMode"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static byte[] Encrypt(string original, string key, string vector, CipherMode cipherMode = CipherMode.CBC,
        PaddingMode paddingMode = PaddingMode.PKCS7, Encoding? encoding = null)
    {
        var bKey = (encoding ?? Encoding).GetBytes(key);
        var bVector = (encoding ?? Encoding).GetBytes(vector);
        return Encrypt(original, bKey, bVector, cipherMode, paddingMode);
    }

    /// <summary>
    /// DES Encrypt
    /// </summary>
    /// <param name="original"></param>
    /// <param name="key"></param>
    /// <param name="vector"></param>
    /// <param name="cipherMode"></param>
    /// <param name="paddingMode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public static byte[] Encrypt(string original, byte[] key, byte[] vector, CipherMode cipherMode = CipherMode.CBC,
        PaddingMode paddingMode = PaddingMode.PKCS7)
    {
        Array.Resize(ref key, 8);
        Array.Resize(ref vector, 8);
        using (var des = DES.Create())
        {
            des.Mode = cipherMode;
            des.Padding = paddingMode;
            using (var encryptor = des.CreateEncryptor(key, vector))
            using (var msEncrypt = new MemoryStream())
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (var swEncrypt = new StreamWriter(csEncrypt))
                    swEncrypt.Write(original);
                return msEncrypt.ToArray();
            }
        }
    }

    /// <summary>
    /// DES Decrypt
    /// </summary>
    /// <param name="encrypted"></param>
    /// <param name="key"></param>
    /// <param name="vector"></param>
    /// <param name="cipherMode"></param>
    /// <param name="paddingMode"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string Decrypt(byte[] encrypted, string key, string vector, CipherMode cipherMode = CipherMode.CBC,
        PaddingMode paddingMode = PaddingMode.PKCS7, Encoding? encoding = null)
    {
        var bKey = (encoding ?? Encoding).GetBytes(key);
        var bVector = (encoding ?? Encoding).GetBytes(vector);
        return Decrypt(encrypted, bKey, bVector, cipherMode, paddingMode);
    }

    /// <summary>
    /// DES Decrypt
    /// </summary>
    /// <param name="encrypted"></param>
    /// <param name="key"></param>
    /// <param name="vector"></param>
    /// <param name="cipherMode"></param>
    /// <param name="paddingMode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public static string Decrypt(byte[] encrypted, byte[] key, byte[] vector, CipherMode cipherMode = CipherMode.CBC,
        PaddingMode paddingMode = PaddingMode.PKCS7)
    {
        Array.Resize(ref key, 8);
        Array.Resize(ref vector, 8);
        using (var des = DES.Create())
        {
            des.Mode = cipherMode;
            des.Padding = paddingMode;
            using (var decryptor = des.CreateDecryptor(key, vector))
            using (var msDecrypt = new MemoryStream(encrypted))
            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (var srDecrypt = new StreamReader(csDecrypt))
                return srDecrypt.ReadToEnd();
        }
    }
}