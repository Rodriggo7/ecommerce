namespace ECommerce.Infrastructure.Security;

using ECommerce.Application.Interfaces;
using BCrypt.Net;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Verify(password, hash);
    }
}