using BusinessLayer.ResponseModels.ViewModels;
using HMS_BE.Models.PagingModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface IUserRepository
    {
        Task<BasePagingModel<HMS_BE.DTO.User>> GetUserList(PagingModel paging);
        Task<HMS_BE.DTO.User> GetUserById(int id);
        Task AddUser(HMS_BE.DTO.User user);
        Task UpdateUser(HMS_BE.DTO.User user);
        Task DeleteUser(int id);
    }
}
