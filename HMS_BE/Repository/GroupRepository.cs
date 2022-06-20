using HMS_BE.DAO;
using HMS_BE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public class GroupRepository : IGroupRepository
    {
        public Task AddGroup(Group group)
        {
            return GroupDAO.Instance.Add(group);
        }

        public Task DeleteGroup(int id)
        {
            return GroupDAO.Instance.Delete(id);
        }

        public Task<IEnumerable<Group>> GetAvalableGroupList()
        {
            return GroupDAO.Instance.GetAvailableGroup();
        }

        public Task<Group> GetGroupById(int id)
        {
            return GroupDAO.Instance.Get(id);
        }

        public Task<IEnumerable<Group>> GetGroupList()
        {
            return GroupDAO.Instance.Get();
        }

        public Task UpdateGroup(Group group)
        {
            return GroupDAO.Instance.Update(group);
        }
    }
}
