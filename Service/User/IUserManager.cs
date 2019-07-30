using Service.User.AccountService;
using Service.User.UserService;

namespace Service.User
{
    public partial interface IUserManager
    {
        IAccountService AccountService { get; }
        IUserService UserService { get; }

    }
}
