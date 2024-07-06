namespace Server.Shared.Password;

public static class Encryption
{
    public static string EncryptPassword(string pPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(pPassword);
    }

    public static bool VerifyPassword(string pPassword, string pHash)
    {
        return BCrypt.Net.BCrypt.Verify(pPassword, pHash);
    }
}
