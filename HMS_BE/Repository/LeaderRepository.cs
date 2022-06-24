using AutoMapper;
using HMS_BE.DAO;
using HMS_BE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public class LeaderRepository : ILeaderRepository
    {
        private readonly IMapper _mapper;

        public LeaderRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Leader> GetLeaderByGroupId(int id)
        {
            var leader = await LeaderDAO.Instance.GetLeaderByGroupId(id);
            return _mapper.Map<HMS_BE.DTO.Leader>(leader);
        }
    }
}
