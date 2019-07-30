using System;
using System.Threading.Tasks;
using System.Linq;
using System.Configuration;
using Core.Contract;
using Core.Helper;
using Core.Object;
using Data;
using Data.Database;


namespace Service.User.AccountService
{
    public partial class AccountService : IAccountService
    {
        #region Fields and Constructor
        private readonly IRepository<tUser> userRepository;
        private readonly IRepository<tUserLogin> userLoginRepository;

        public AccountService(
            IRepository<tUser> userRepository,
            IRepository<tUserLogin> userLoginRepository)
        {
            this.userRepository = userRepository;
            this.userLoginRepository = userLoginRepository;
        }
        #endregion

        #region Methods
        public async Task<ObjectResult<LoginResult>> LoginAsync(LoginContract user)
        {
            ObjectResult<LoginResult> result = new ObjectResult<LoginResult>();
            try
            {
                string _password = HelperMethod.encrypt(user.Password);
                var query = userRepository.GetTable.Where(d => d.Username.Equals(user.Username) && d.Password.Equals(_password)).AsQueryable();
                var data = await userRepository.GetDataAsync(query);
                if (data != null)
                {
                    ServiceResult loginResult = new ServiceResult();
                    loginResult.OK();
                    var loginQuery = userLoginRepository.GetTable.Where(d => d.UserName.Equals(user.Username) && d.DeviceID.Equals(user.DeviceID)).AsQueryable();
                    if (await userLoginRepository.GetCountAsync(loginQuery) == 0)
                    {
                        tUserLogin _loginData = new tUserLogin
                        {
                            UserLoginID = HelperMethod.GenerateID(),
                            UserName = user.Username,
                            DeviceID = user.DeviceID,
                            Token = HelperMethod.GenerateID().ToString()
                        };
                        var saveLogin = await userLoginRepository.AddAsync(_loginData);
                        loginResult = saveLogin.status;
                    }
                    if (loginResult.succeeded)
                    {
                        var loginData = await userLoginRepository.GetDataAsync(loginQuery);
                        result.obj = GetLoginResult(loginData);
                        result.status.OK();
                    }
                    else
                        result.status = loginResult;
                }
                else
                {
                    result.status.NotFound("Data Not Found");
                }
            }
            catch (Exception ex)
            {
                result.status.Error("Error", ex.Message);
            }
            return result;
        }
        public async Task<ServiceResult> LogoutAsync(LogoutContract user)
        {
            ServiceResult result = new ServiceResult();
            try
            {

                tUserLogin data = await userLoginRepository.GetDataAsync(userLoginRepository.GetTable.Where(d => d.Token.Equals(user.Token) && d.DeviceID.Equals(user.DeviceID)).AsQueryable());
                if (data != null)
                    result = await userLoginRepository.RemoveAsync(data);
                else
                    result.NotFound("Data Not Found");
            }
            catch (Exception ex)
            {
                result.Error("Error", ex.Message);
            }
            return result;
        }

        public ObjectResult<LoginResult> CheckToken(string token)
        {
            ObjectResult<LoginResult> result = new ObjectResult<LoginResult>();
            try
            {
                var query = userLoginRepository.GetTable.Where(d => d.Token.Equals(token)).AsEnumerable();
                var data = query.SingleOrDefault();
                if (data != null)
                {
                    result.obj = GetLoginResult(data);
                    result.status.OK();
                }
                else
                    result.status.UnAuthorized();
            }
            catch (Exception ex)
            {
                result.status.Error("Error", ex.Message);
            }
            return result;
        }
        #endregion

        #region Utilities
        private LoginResult GetLoginResult(tUserLogin d)
        {
            return new LoginResult
            {
                DeviceID = d.DeviceID,
                Token = d.Token,
                User = new MiniProfile
                {
                    UserName = d.UserName
                }
            };
        }
        #endregion
    }
}
