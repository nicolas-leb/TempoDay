
namespace DataLayer.Auth
{
    public interface IAuthService
    {
        Task<Token> AuthenticateAsync();
    }
}