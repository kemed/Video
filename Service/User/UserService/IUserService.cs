using System;
using System.Threading.Tasks;
using Core.Contract;
using Core.Object;
using System.IO;

namespace Service.User.UserService
{
    public partial interface IUserService
    {
        Task<ServiceResult> AddAsync(AddUserContract user);
        Task<ServiceResult> RemoveAsync(string UserID);
        Task<ListPageResult<MiniProfile>> GetUsersAsync(int page, int pageSize);
        Task<ListPageResult<MiniProfile>> FindUserByNameAsync(string search, int page, int pageSize);
    }
}
