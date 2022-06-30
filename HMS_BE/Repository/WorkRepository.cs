using AutoMapper;
using HMS_BE.DAO;
using HMS_BE.DTO;
using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
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

        public async Task<BasePagingModel<WorkModel>> GetWorkById(WorkSearchModel searchModel, PagingModel paging)
        {
            var wgrs = await WorkDAO.Instance.GetWorkById(searchModel.workId);
            List<HMS_BE.DTO.Work> workList = _mapper.Map<IEnumerable<HMS_BE.DTO.Work>>(wgrs).ToList();
            int totalItem = workList.ToList().Count;

            workList = workList.Skip((paging.PageIndex - 1) * paging.PageSize)
                .Take(paging.PageSize).ToList();

            var workModelList = new List<HMS_BE.DTO.WorkModel>();

            foreach (var wrk in workList)
            {
                workModelList.Add(new HMS_BE.DTO.WorkModel()
                {
                    Work = wrk,
                    WorkTicket = _mapper.Map<HMS_BE.DTO.WorkTicket>(await WorkTicketDAO.Instance.Get((int)wrk.Id))
                });
            }

            var workResult = new BasePagingModel<HMS_BE.DTO.WorkModel>()
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalItem = totalItem,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize),
                Data = workModelList
            };

            return workResult;
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
