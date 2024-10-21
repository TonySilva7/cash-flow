using System;
using System.Text;
using System.Security.Cryptography;
using CashFlow.Domain.Security.Cryptography;

namespace CashFlow.Application.UseCases;

public class PasswordEncrypter : IPasswordEncrypter
{

    public string Encrypt(string password)
    {
        var aditionalKey = "CashFlow";
        var newPass = $"{password}{aditionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newPass);
        var hash = SHA512.HashData(bytes);
        return ByteArrayToString(hash);
    }


    private static string ByteArrayToString(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }

}
