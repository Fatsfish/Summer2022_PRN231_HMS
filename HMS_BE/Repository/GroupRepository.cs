using AutoMapper;
using HMS_BE.DAO;
using HMS_BE.DTO;
using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace HMS_BE.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMapper _mapper;

        public GroupRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task AddGroup(HMS_BE.DTO.Group group)
        {
            var gr = _mapper.Map<HMS_BE.Models.Group>(group);
            await GroupDAO.Instance.Add(gr);
            return;
        }

        public async Task DeleteGroup(int id)
        {
            await GroupDAO.Instance.Delete(id);
            return;
        }

        public async Task<HMS_BE.DTO.Group> GetGroupById(int id)
        {
            var group = await GroupDAO.Instance.Get(id);
            return _mapper.Map<HMS_BE.DTO.Group>(group);
        }

        public async Task<BasePagingModel<HMS_BE.DTO.Group>> GetGroupList(GroupSearchModel searchModel, PagingModel paging)
        {
            var list = await GroupDAO.Instance.Get();
            List<HMS_BE.DTO.Group> groupList = _mapper.Map<IEnumerable<HMS_BE.DTO.Group>>(list).ToList();


            // Search for user list
            groupList = groupList.Where(x => StringNormalizer.VietnameseNormalize(x.Name)
                            .Contains(StringNormalizer.VietnameseNormalize(searchModel.SearchTerm)))
                        .Where(x => (searchModel.isDelete != null) ? x.IsDelete == (bool)searchModel.isDelete
                                            : true)
                        .ToList();

            // Calculate total item
            int totalItem = groupList.ToList().Count;

            groupList = groupList.Skip((paging.PageIndex - 1) * paging.PageSize)
                .Take(paging.PageSize).ToList();

            var groupResult = new BasePagingModel<HMS_BE.DTO.Group>()
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalItem = totalItem,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize),
                Data = groupList
            };

            return groupResult;
        }

        public async Task<BasePagingModel<Group>> GetGroupListByUserEmail(UserGroupSearchModel searchModel, PagingModel paging)
        {
            var user = await UserDAO.Instance.GetUserByEmail(searchModel.Email);
            if(user == null)
            {
                var nothing = new BasePagingModel<HMS_BE.DTO.Group>()
                {
                    PageIndex = paging.PageIndex,
                    PageSize = paging.PageSize,
                    TotalItem = 0,
                    TotalPage = (int)Math.Ceiling((decimal)0 / (decimal)paging.PageSize),
                    Data = new List<HMS_BE.DTO.Group>()
                };
                return nothing;
            }
            var groupUsers = await GroupUserDAO.Instance.GetGroupUserByUserId(user.Id);
            List<HMS_BE.DTO.Group> groupList = new List<HMS_BE.DTO.Group>();
            foreach(var groupUser in groupUsers)
            {
                var group = await GroupDAO.Instance.Get((int)groupUser.GroupId);
                groupList.Add(_mapper.Map<HMS_BE.DTO.Group>(group));
            }
            groupList.ToList();
            // Search for user list
            groupList = groupList.Where(x => StringNormalizer.VietnameseNormalize(x.Name)
                            .Contains(StringNormalizer.VietnameseNormalize(searchModel.SearchTerm)))
                        .Where(x => x.IsDelete == false)
                        .ToList();

            // Calculate total item
            int totalItem = groupList.ToList().Count;

            groupList = groupList.Skip((paging.PageIndex - 1) * paging.PageSize)
                .Take(paging.PageSize).ToList();

            var groupResult = new BasePagingModel<HMS_BE.DTO.Group>()
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalItem = totalItem,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize),
                Data = groupList
            };

            return groupResult;
        }

        public Task UpdateGroup(HMS_BE.DTO.Group group)
        {
            var gr = _mapper.Map<HMS_BE.Models.Group>(group);
            return GroupDAO.Instance.Update(gr);
        }
    }
}
