using AutoMapper;
using HMS_BE.DAO;
using HMS_BE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public class WorkRepository : IWorkRepository
    {
        private readonly IMapper _mapper;

        public WorkRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task AddWork(Work work)
        {
            var workModel = _mapper.Map<Work, Models.Work>(work);
            await WorkDAO.Instance.Add(workModel);
        }

        public async Task DeleteWork(int id)
        {
            await WorkDAO.Instance.Delete(id);
        }

        public async Task<IEnumerable<Work>> GetAvalableWorkList()
        {
            var workList = await WorkDAO.Instance.GetAvailableWork();
            var data = _mapper.Map<IEnumerable<Models.Work>, IEnumerable<Work>>(workList);
            return data;
        }

        public async Task<HMS_BE.DTO.Work> GetWorkById(int id)
        {
            var w = await WorkDAO.Instance.Get(id);
            return _mapper.Map<HMS_BE.DTO.Work>(w);
        }
    }
}
