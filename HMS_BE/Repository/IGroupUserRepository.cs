using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface IGroupUserRepository
    {
        Task<IEnumerable<HMS_BE.DTO.GroupUser>> GetGroupUsersByGroupId(int id);
        Task<IEnumerable<HMS_BE.DTO.GroupUser>> GetConditionGroupUsersByGroupId(int id, bool condition);
        Task<HMS_BE.DTO.GroupUser> GetGroupUserByID(int id);
        Task RemoveGroupUser(int id);
        Task AddGroupUser(HMS_BE.DTO.GroupUser groupUser);
    }
}
