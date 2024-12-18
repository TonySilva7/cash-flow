using CashFlow.Domain.Security.Cryptography;
namespace CashFlow.Infrastructure.Security.Cryptography;

public class BCryptNet : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool IsValidPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
