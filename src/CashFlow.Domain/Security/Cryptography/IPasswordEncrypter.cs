using System;

namespace CashFlow.Domain.Security.Cryptography;

public interface IPasswordEncrypter
{
    string Encrypt(string password);
    bool IsValidPassword(string password, string hash);
}
