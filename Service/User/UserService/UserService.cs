using System;
using System.Threading.Tasks;
using System.Linq;
using System.Configuration;
using System.IO;
using Core.Contract;
using Core.Helper;
using Core.Object;
using Data;
using Data.Database;

namespace Service.User.UserService
{
    public partial class UserService : IUserService
    {
        #region Fields and Constructor
        private readonly IRepository<tUser> userRepository;
        private readonly IRepository<tUserLogin> userLoginRepository;

        public UserService(
            IRepository<tUser> userRepository,
            IRepository<tUserLogin> userLoginRepository)
        {
            this.userRepository = userRepository;
            this.userLoginRepository = userLoginRepository;
        }
        #endregion

        #region Methods
        public async Task<ServiceResult> AddAsync(AddUserContract user)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var check = await userRepository.GetCountAsync(userRepository.GetTable.Where(d => d.UserID.Equals(user.UserID)).AsQueryable());
                if (check == 0)
                {
                    tUser data = new tUser
                    {
                        UserID = Guid.Parse(user.UserID.ToString()),
                        Password = HelperMethod.encrypt(user.Password),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    };
                    var save = await userRepository.AddAsync(data);
                    result = save.status;
                }
                else
                    result.BadRequest("REGISTERED_ALREADY");
                return result;
            }
            catch (Exception ex)
            {
                result.Error("ERROR", ex.Message);
            }
            return result;
        }

        public async Task<ServiceResult> RemoveAsync(string userID)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                tUser data = await userRepository.GetDataAsync(userRepository.GetTable.Where(d => d.UserID.Equals(userID)).AsQueryable());
                if (data != null)
                    result = await userRepository.RemoveAsync(data);
                else
                    result.NotFound("NOT_FOUND");
            }
            catch (Exception ex)
            {
                result.Error("ERROR", ex.Message);
            }
            return result;
        }
        
        public async Task<ListPageResult<MiniProfile>> GetUsersAsync(int page, int pageSize)
        {
            ListPageResult<MiniProfile> result = new ListPageResult<MiniProfile>();
            try
            {
                var query = userRepository.GetTable.OrderByDescending(d => d.UpdateDate).Skip((page - 1) * pageSize).Take(pageSize).AsQueryable();
                var data = await userRepository.GetListAsync(query);
                result.listObj = data.Select(d => GetUserData(d)).ToList();
                result.count = await userRepository.GetCountAsync(userRepository.GetTable.AsQueryable());
                result.status.OK();
            }
            catch (Exception ex)
            {
                result.status.Error("ERROR", ex.Message);
            }
            return result;
        }

        public async Task<ListPageResult<MiniProfile>> FindUserByNameAsync(string search, int page, int pageSize)
        {
            ListPageResult<MiniProfile> result = new ListPageResult<MiniProfile>();
            try
            {
                var query = userRepository.GetTable.Where(d => d.FirstName.Contains(search) || d.LastName.Contains(search)).OrderByDescending(d => d.UpdateDate).Skip((page - 1) * pageSize).Take(pageSize).AsQueryable();
                var data = await userRepository.GetListAsync(query);
                result.listObj = data.Select(d => GetUserData(d)).ToList();
                result.count = await userRepository.GetCountAsync(userRepository.GetTable.Where(d => d.FirstName.Contains(search) || d.LastName.Contains(search)).AsQueryable());
                result.status.OK();
            }
            catch (Exception ex)
            {
                result.status.Error("ERROR", ex.Message);
            }
            return result;
        }

        #endregion

        #region Utilities
        [Obsolete]
        private ObjectResult<string> UploadFile(string nip, string filetype, Stream file)
        {
            ObjectResult<string> result = new ObjectResult<string>();
            if (string.IsNullOrWhiteSpace(filetype))
            {
                result.status.BadRequest("UNKNOWN_FILE_TYPE");
                return result;
            }
            if (!HelperMethod.isValidVideo(filetype))
            {
                result.status.BadRequest("NOT_VIDEO_FILETYPE");
                return result;
            }
            try
            {
                string dir = Path.GetFullPath(ConfigurationSettings.AppSettings["VideoPath"].ToString());
                string target = Path.Combine(dir, nip + filetype);
                bool exists = Directory.Exists(dir);
                if (!exists)
                    System.IO.Directory.CreateDirectory(dir);
                using (FileStream output = new FileStream(target, FileMode.Create))
                {
                    file.CopyTo(output);
                }
                result.obj = nip + filetype;
                result.status.OK();
            }
            catch (Exception ex)
            {
                result.status.Error("FAILED_UPLOAD", ex.Message);
            }
            return result;
        }
        private MiniProfile GetUserData(tUser d)
        {
            return new MiniProfile
            {
                UserName = d.Username,
                FirstName = d.FirstName,
                LastName = d.LastName,
                UserID = d.UserID.ToString()
            };
        }
      
        #endregion
    }
}
