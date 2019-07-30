using Autofac;
using Service.DependencyManager;
using Service.User;

namespace Service
{
    public sealed partial class ServiceManager
    {
        #region Field and Initialization
        private static ServiceManager _instance = null;
        private static readonly object SyncRoot = new object();
        private IContainer Container { get; set; }
        private ServiceManager() { CreateInstance(); }
        public static ServiceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        _instance = _instance ?? new ServiceManager();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region Dependency Injection
        private void CreateInstance()
        {
            Container = DependencyRegister.CreateDependency();
        }
        private T GetDependency<T>()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<T>();
                return writer;
            }
        }
        #endregion

        #region Services
        public IUserManager UserManager
        {
            get { return GetDependency<IUserManager>(); }
        }
        #endregion
    }
}
