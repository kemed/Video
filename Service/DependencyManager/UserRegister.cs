using Autofac;
using Service.User.AccountService;
using Service.User.UserService;

namespace Service.DependencyManager
{
    public static partial class DependencyRegister
    {
        private static ContainerBuilder GetUserDependency(ContainerBuilder builder)
        {
            // user register
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<UserService>().As<IUserService>();
            return builder;
        }
    }
}
