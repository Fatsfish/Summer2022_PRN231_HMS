using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface IGroupRepository
    {
        Task<IEnumerable<HMS_BE.DTO.Group>> GetGroupList();
        Task<HMS_BE.DTO.Group> GetGroupById(int id);
        Task AddGroup(HMS_BE.DTO.Group group);
        Task UpdateGroup(HMS_BE.DTO.Group group);
        Task DeleteGroup(int id);

    }
}
