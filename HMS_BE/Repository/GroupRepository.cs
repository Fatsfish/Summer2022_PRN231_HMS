using AutoMapper;
using HMS_BE.DAO;
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

        public Task UpdateGroup(HMS_BE.DTO.Group group)
        {
            var gr = _mapper.Map<HMS_BE.Models.Group>(group);
            return GroupDAO.Instance.Update(gr);
        }
    }
}
