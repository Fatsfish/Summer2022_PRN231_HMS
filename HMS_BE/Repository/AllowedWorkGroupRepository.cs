using AutoMapper;
using HMS_BE.DAO;
using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public class AllowedWorkGroupRepository : IAllowedWorkGroupRepository
    {
        private readonly IMapper _mapper;

        public AllowedWorkGroupRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<BasePagingModel<HMS_BE.DTO.AllowedWorkGroupModel>> GetAllowedWorkGroupsByGroupID(AllowedWorkGroupSearchModel searchModel, PagingModel paging)
        {
            var wgrs = await AllowedWorkGroupDAO.Instance.GetAllowedWorkGroupByGroupId(searchModel.groupId);
            List<HMS_BE.DTO.AllowedWorkGroup> allowWorkGroupList = _mapper.Map<IEnumerable<HMS_BE.DTO.AllowedWorkGroup>>(wgrs).ToList();

            int totalItem = allowWorkGroupList.ToList().Count;

            allowWorkGroupList = allowWorkGroupList.Skip((paging.PageIndex - 1) * paging.PageSize)
                .Take(paging.PageSize).ToList();

            var allowedWorkGroupModelList = new List<HMS_BE.DTO.AllowedWorkGroupModel>();

            foreach (var awg in allowWorkGroupList)
            {
                allowedWorkGroupModelList.Add(new HMS_BE.DTO.AllowedWorkGroupModel()
                {
                    AllowedWorkGroup = awg,
                    Work = _mapper.Map<HMS_BE.DTO.Work>(await WorkDAO.Instance.Get((int)awg.WorkId))
                });
            }

            var allowedWorkGroupResult = new BasePagingModel<HMS_BE.DTO.AllowedWorkGroupModel>()
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalItem = totalItem,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize),
                Data = allowedWorkGroupModelList
            };

            return allowedWorkGroupResult;
        }
    }
}
