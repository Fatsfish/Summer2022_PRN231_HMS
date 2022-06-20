using HMS_BE.DAO;
using HMS_BE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public class GroupUserRepository : IGroupUserRepository
    {
        public Task<GroupUser> GetGroupUserByID(int id)
        {
            return GroupUserDAO.Instance.GetGroupUserByID(id);
        }

        public Task<IEnumerable<GroupUser>> GetGroupUsersByGroupId(int id)
        {
            return GroupUserDAO.Instance.GetGroupUserByGroupId(id);
        }

        public Task RemoveGroupUser(int id)
        {
            return GroupUserDAO.Instance.Delete(id);
        }
    }
}
