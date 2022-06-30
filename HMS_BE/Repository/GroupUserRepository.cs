using AutoMapper;
using HMS_BE.DAO;
using HMS_BE.Models.PagingModel;
using HMS_BE.Models.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace HMS_BE.Repository
{
    public class GroupUserRepository : IGroupUserRepository
    {
        private readonly IMapper _mapper;

        public GroupUserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task AddGroupUser(HMS_BE.DTO.GroupUser groupUser)
        {
            var gr = _mapper.Map<HMS_BE.Models.GroupUser>(groupUser);
            await GroupUserDAO.Instance.Add(gr);
            return;
        }

        public async Task AddListGroupUser(IEnumerable<HMS_BE.DTO.GroupUser> groupUsers)
        {
            var grs = _mapper.Map<IEnumerable<HMS_BE.Models.GroupUser>>(groupUsers);
            await GroupUserDAO.Instance.AddList(grs);
            return;
        }

        public async Task<BasePagingModel<HMS_BE.DTO.GroupUserRequestModel>> GetConditionGroupUsersByGroupId(GroupUserSearchModel searchModel, PagingModel paging)
        {
            var list = await GroupUserDAO.Instance.GetConditionGroupUserByGroupId(searchModel.id, searchModel.condition);
            List<HMS_BE.DTO.GroupUser> groupUserList = _mapper.Map<IEnumerable<HMS_BE.DTO.GroupUser>>(list).ToList();


            //// Search for user list
            //groupUserList = groupUserList.Where(x => StringNormalizer.VietnameseNormalize(x.Name)
            //                .Contains(StringNormalizer.VietnameseNormalize(searchModel.SearchTerm)))
            //            .Where(x => (searchModel.isDelete != null) ? x.IsDelete == (bool)searchModel.isDelete
            //                                : true)
            //            .ToList();

            // Calculate total item
            int totalItem = groupUserList.ToList().Count;

            groupUserList = groupUserList.Skip((paging.PageIndex - 1) * paging.PageSize)
                .Take(paging.PageSize).ToList();

            var groupUserRequestList = new List<HMS_BE.DTO.GroupUserRequestModel>();

            var userList = new List<HMS_BE.Models.User>();
            var groupList = new List<HMS_BE.Models.Group>();
            foreach(var gu in groupUserList)
            {
                groupUserRequestList.Add(new HMS_BE.DTO.GroupUserRequestModel()
                {
                    groupUser = gu,
                    user = _mapper.Map<HMS_BE.DTO.User>(await UserDAO.Instance.Get((int)gu.UserId)),
                    group = _mapper.Map<HMS_BE.DTO.Group>(await GroupDAO.Instance.Get((int)gu.GroupId))
                });
            }

            var groupUserResult = new BasePagingModel<HMS_BE.DTO.GroupUserRequestModel>()
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalItem = totalItem,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize),
                Data = groupUserRequestList
            };

            return groupUserResult;
        }

        public async Task<HMS_BE.DTO.GroupUser> GetGroupUserByID(int id)
        {
            var groupuser = await GroupUserDAO.Instance.GetGroupUserByID(id);
            return _mapper.Map<HMS_BE.DTO.GroupUser>(groupuser);
        }

        public async Task<IEnumerable<HMS_BE.DTO.GroupUser>> GetGroupUsersByGroupId(int id)
        {
            var list = await GroupUserDAO.Instance.GetGroupUserByGroupId(id);
            return _mapper.Map<IEnumerable<HMS_BE.DTO.GroupUser>>(list);
        }

        public Task RemoveGroupUser(int id)
        {
            return GroupUserDAO.Instance.Delete(id);
        }

        public async Task UpdateGroupUser(DTO.GroupUser groupUser)
        {
            var gr = _mapper.Map<HMS_BE.Models.GroupUser>(groupUser);
            await GroupUserDAO.Instance.Update(gr);
            return;
        }
    }
}
