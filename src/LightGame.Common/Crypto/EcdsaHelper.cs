using System.Security.Cryptography;
using System.Text;

namespace LightGame.Common;

public static class EcdsaHelper
{
    public static Encoding Encoding { get; set; } = Encoding.UTF8;
    public static HashAlgorithmName HashAlgorithmName { get; set; } = HashAlgorithmName.SHA256;

    #region Data

    public static byte[] SignData(string original, ECParameters privateKey,
        HashAlgorithmName? hashAlgorithmName = null, Encoding? encoding = null) =>
        SignData((encoding ?? Encoding).GetBytes(original), privateKey, hashAlgorithmName);

    public static byte[] SignData(byte[] original, ECParameters privateKey,
        HashAlgorithmName? hashAlgorithmName = null)
    {
        using var ecDsa = ECDsa.Create();
        ecDsa.ImportParameters(privateKey);
        return ecDsa.SignData(original, hashAlgorithmName ?? HashAlgorithmName);
    }

    public static bool VerifyData(string original, byte[] signature, ECParameters publicKey,
        HashAlgorithmName? hashAlgorithmName = null, Encoding? encoding = null) =>
        VerifyData((encoding ?? Encoding).GetBytes(original), signature, publicKey, hashAlgorithmName);

    public static bool VerifyData(byte[] original, byte[] signature, ECParameters publicKey,
        HashAlgorithmName? hashAlgorithmName = null)
    {
        using var ecDsa = ECDsa.Create();
        ecDsa.ImportParameters(publicKey);
        return ecDsa.VerifyData(original, signature, hashAlgorithmName ?? HashAlgorithmName);
    }

    #endregion

    #region Hash

    public static byte[] SignHash(string original, ECParameters privateKey, Encoding? encoding = null) =>
        SignHash((encoding ?? Encoding).GetBytes(original), privateKey);

    public static byte[] SignHash(byte[] original, ECParameters privateKey)
    {
        using var ecDsa = ECDsa.Create();
        ecDsa.ImportParameters(privateKey);
        return ecDsa.SignHash(original);
    }

    public static bool VerifyHash(string original, byte[] signature, ECParameters publicKey,
        Encoding? encoding = null) =>
        VerifyHash((encoding ?? Encoding).GetBytes(original), signature, publicKey);

    public static bool VerifyHash(byte[] original, byte[] signature, ECParameters publicKey)
    {
        using var ecDsa = ECDsa.Create();
        ecDsa.ImportParameters(publicKey);
        return ecDsa.VerifyHash(original, signature);
    }

    #endregion

    public static (ECParameters privateKey, ECParameters publicKey) GenerateParameters()
    {
        using var ecDsa = ECDsa.Create();
        var privateKey = ecDsa.ExportParameters(true);
        var publicKey = ecDsa.ExportParameters(false);
        return (privateKey, publicKey);
    }
}