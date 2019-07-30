using System;
using System.Threading.Tasks;
using Core.Contract;
using Core.Object;
using Video.Areas.API.Helper;
using System.Runtime.ExceptionServices;
using Service;

namespace Video.Areas.API
{

    public partial class VideoService : IUserAPI
    {
        public IAsyncResult BeginLogin(string format, LoginContract user, AsyncCallback callback, object asyncState)
        {
            return APIHelper.WrapperService(format, callback, asyncState, true, delegate()
            {
                return ServiceManager.Instance.UserManager.AccountService.LoginAsync(user);
            });
        }

        public ObjectResult<LoginResult> EndLogin(IAsyncResult result)
        {
            try
            {
                return ((Task<ObjectResult<LoginResult>>)result).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        public IAsyncResult BeginLogout(string format, LogoutContract user, AsyncCallback callback, object asyncState)
        {
            return APIHelper.WrapperService(format, callback, asyncState, true, delegate()
            {
                return ServiceManager.Instance.UserManager.AccountService.LogoutAsync(user);
            });
        }

        public ServiceResult EndLogout(IAsyncResult result)
        {
            try
            {
                return ((Task<ServiceResult>)result).Result;
            }
            catch (AggregateException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

    }
}
