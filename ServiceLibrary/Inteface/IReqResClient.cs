using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Inteface
{
    public interface IReqResClient
    {
        Task<UserPageResponse> GetUsersAsync(int page);
        Task<UserData?> GetUserByIdAsync(int userId);
    }
}
