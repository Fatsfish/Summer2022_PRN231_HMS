using AutoMapper;
using HMS_BE.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<HMS_BE.DTO.Group>> GetAvalableGroupList()
        {
            var list = await GroupDAO.Instance.GetAvailableGroup();
            return _mapper.Map<IEnumerable<HMS_BE.DTO.Group>>(list);
        }

        public async Task<HMS_BE.DTO.Group> GetGroupById(int id)
        {
            var group = await GroupDAO.Instance.Get(id);
            return _mapper.Map<HMS_BE.DTO.Group>(group);
        }

        public async Task<IEnumerable<HMS_BE.DTO.Group>> GetGroupList()
        {
            var list = await GroupDAO.Instance.Get();
            return _mapper.Map<IEnumerable<HMS_BE.DTO.Group>>(list);
        }

        public Task UpdateGroup(HMS_BE.DTO.Group group)
        {
            var gr = _mapper.Map<HMS_BE.Models.Group>(group);
            return GroupDAO.Instance.Update(gr);
        }
    }
}
