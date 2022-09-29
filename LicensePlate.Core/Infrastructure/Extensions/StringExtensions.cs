using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LicensePlate.Core.Infrastructure.Extensions;

public static class StringExtensions
{
    internal static SymmetricSecurityKey ConvertToSymmetricSecurityKey(this string value)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(value));
    }

    internal static string GenerateOAuthHash(this string input)
    {
        HashAlgorithm hashAlgorithm = SHA256.Create();
        var byteValue = Encoding.UTF8.GetBytes(input);
        var byteHash = hashAlgorithm.ComputeHash(byteValue);
        return Convert.ToBase64String(byteHash);
    }

    internal static string Encrypt(this string input, string key)
    {
        byte[] inputArray = Encoding.UTF8.GetBytes(input);
        TripleDES tripleDES = CreateTripleDES(key);

        ICryptoTransform cTransform = tripleDES.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();

        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    internal static string Decrypt(this string input, string key)
    {
        byte[] inputArray = Convert.FromBase64String(input);
        TripleDES tripleDES = CreateTripleDES(key);

        ICryptoTransform cTransform = tripleDES.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();

        return Encoding.UTF8.GetString(resultArray);
    }

    private static TripleDES CreateTripleDES(string key)
    {
        TripleDES tripleDES = TripleDES.Create();

        byte[] bytes = Encoding.ASCII.GetBytes(key);

        tripleDES.Key = bytes;
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;
        return tripleDES;
    }
}