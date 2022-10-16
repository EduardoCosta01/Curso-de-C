using ApiCatalogo.Models;

namespace ApiCatalogo.Services
{
    public interface ITokenService
    {
        string GerarToken(string Key, string issuer, string audience, UserModel user);
    }
}
