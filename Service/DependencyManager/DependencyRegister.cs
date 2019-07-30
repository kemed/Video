using Autofac;
using Data;
using Service.User;


namespace Service.DependencyManager
{
    public static partial class DependencyRegister
    {
        public static IContainer CreateDependency()
        {
            var builder = new ContainerBuilder();
            // global register
            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>));

            builder = GetUserDependency(builder);

            IContainer Container = builder.Build();
            return Container;
        }
    }
}
