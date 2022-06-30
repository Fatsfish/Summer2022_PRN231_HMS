
using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface IGroupUserRepository
    {
        Task<IEnumerable<HMS_BE.DTO.GroupUser>> GetGroupUsersByGroupId(int id);
        Task<BasePagingModel<HMS_BE.DTO.GroupUserRequestModel>> GetConditionGroupUsersByGroupId(GroupUserSearchModel searchModel, PagingModel paging);
        Task<HMS_BE.DTO.GroupUser> GetGroupUserByID(int id);
        Task RemoveGroupUser(int id);
        Task AddGroupUser(HMS_BE.DTO.GroupUser groupUser);
        Task UpdateGroupUser(HMS_BE.DTO.GroupUser groupUser);
        Task AddListGroupUser(IEnumerable<HMS_BE.DTO.GroupUser> groupUsers);
    }
}
