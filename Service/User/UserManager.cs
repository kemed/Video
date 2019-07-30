using System;
using Service.User.AccountService;
using Service.User.UserService;

namespace Service.User
{
    public partial class UserManager : IUserManager
    {
        #region Fields and Constructor
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        public UserManager(
            IAccountService accountService,
            IUserService userService
            )
        {
            this.accountService = accountService;
            this.userService = userService;
        }
        #endregion

        #region Getters
        public IAccountService AccountService
        {
            get { return accountService; }
        }
        public IUserService UserService
        {
            get { return userService; }
        }
        #endregion
    }
}
