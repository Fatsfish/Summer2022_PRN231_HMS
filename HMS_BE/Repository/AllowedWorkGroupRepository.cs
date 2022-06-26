using AutoMapper;
using HMS_BE.DAO;
using HMS_BE.DTO;
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

        public async Task<IEnumerable<AllowedWorkGroup>> GetAllowedWorkGroupsByGroupID(int id)
        {
            var wgrs = await AllowedWorkGroupDAO.Instance.GetAllowedWorkGroupByGroupId(id);
            return _mapper.Map<IEnumerable<HMS_BE.DTO.AllowedWorkGroup>>(wgrs);
        }
    }
}
