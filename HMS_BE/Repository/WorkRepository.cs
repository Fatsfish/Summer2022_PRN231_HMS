using AutoMapper;
using HMS_BE.DAO;
using HMS_BE.DTO;
using System.Collections.Generic;
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

        public async Task<Work> GetWorkById(int id)
        {
            var workModel = await WorkDAO.Instance.Get(id);
            var work = _mapper.Map<Models.Work, Work>(workModel);
            return work;
        }

        public async Task<IEnumerable<Work>> GetWorkList()
        {
            var workModelList = await WorkDAO.Instance.Get();
            var workList = _mapper.Map<IEnumerable<Models.Work>, IEnumerable<Work>>(workModelList);
            return workList;
        }

        public async Task UpdateWork(Work work)
        {
            var workModel = _mapper.Map<Work, Models.Work>(work);
            await WorkDAO.Instance.Update(workModel);
        }
    }
}
