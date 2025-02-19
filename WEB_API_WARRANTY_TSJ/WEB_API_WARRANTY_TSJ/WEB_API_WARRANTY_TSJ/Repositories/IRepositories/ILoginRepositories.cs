using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.ModelsDBERP.Login;

namespace WEB_API_WARRANTY_TSJ.Repositories.IRepositories
{
    public interface ILoginRepositories
    {
        Task<GlobalObjectResponse> LoginUserWeb(LoginRequest parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> LoginUserMobile(LoginRequest parameter, CancellationToken cancellationToken);
        Task<GlobalObjectResponse> RefreshToken(RefreshTokenRequest parameter, CancellationToken cancellationToken);
    }
}
