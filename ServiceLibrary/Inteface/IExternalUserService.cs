using ServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Inteface
{
    public interface IExternalUserService
    {

        Task<UserData> GetUserByIdAsync(int userId);
        Task<IEnumerable<UserData>> GetAllUsersAsync(int page);
    }
}
