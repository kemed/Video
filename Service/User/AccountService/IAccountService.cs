using System;
using System.Threading.Tasks;
using Core.Contract;
using Core.Object;



namespace Service.User.AccountService
{
    public partial interface IAccountService
    {
        Task<ObjectResult<LoginResult>> LoginAsync(LoginContract user);
        Task<ServiceResult> LogoutAsync(LogoutContract user);
        ObjectResult<LoginResult> CheckToken(string token);
    }
}
