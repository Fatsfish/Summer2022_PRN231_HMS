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
    public class WorkTicketRepository : IWorkTicketRepository
    {
        private readonly IMapper _mapper;

        public WorkTicketRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task AddWorkTicket(WorkTicket workTicket)
        {
            var workModel = _mapper.Map<WorkTicket, Models.WorkTicket>(workTicket);
            await WorkTicketDAO.Instance.Add(workModel);
        }

        public async Task<bool> CanLeaveGroup(int id)
        {
            return await WorkTicketDAO.Instance.CanLeaveGroup(id);
        }

        public async Task<IEnumerable<HMS_BE.DTO.WorkTicket>> GetAvailableWorkTicketsByUserID(int id)
        {
            var list = await WorkTicketDAO.Instance.GetAvailableWorkTicketsByUserID(id);
            return _mapper.Map<IEnumerable<HMS_BE.DTO.WorkTicket>>(list);
        }

        public async Task<IEnumerable<WorkTicket>> GetDoneWorkTickets()
        {
            var workList = await WorkTicketDAO.Instance.GetDoneWorkTicket();
            var data = _mapper.Map<IEnumerable<Models.WorkTicket>, IEnumerable<WorkTicket>>(workList);
            return data;
        }

        public async Task<IEnumerable<WorkTicket>> GetWorkTicketsByUserID(int id)
        {
            var workTicketList = await WorkTicketDAO.Instance.GetWorkTicketsByUserID(id);
            var data = _mapper.Map<IEnumerable<Models.WorkTicket>, IEnumerable<WorkTicket>>(workTicketList);
            return data;
        }

        public async Task UpdateWorkTicket(WorkTicket workTicket)
        {
            var workTicketModel = _mapper.Map<WorkTicket, Models.WorkTicket>(workTicket);
            await WorkTicketDAO.Instance.Update(workTicketModel);
        }

        public async Task DeleteWorkTicket(int id)
        {
            await WorkTicketDAO.Instance.Delete(id);
        }

        public async Task<BasePagingModel<DTO.WorkTicket>> GetWorkTickets(WorkTicketSearchModel searchModel, PagingModel paging)
        {
            var list = await WorkTicketDAO.Instance.GetWorkTicketsByUserID(searchModel.workTicketId);
            List<HMS_BE.DTO.WorkTicket> workTicketList = _mapper.Map<IEnumerable<HMS_BE.DTO.WorkTicket>>(list).ToList();

            int totalItem = workTicketList.ToList().Count;

            workTicketList = workTicketList.Skip((paging.PageIndex - 1) * paging.PageSize)
                .Take(paging.PageSize).ToList();

            var workTicketResult = new BasePagingModel<HMS_BE.DTO.WorkTicket>()
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalItem = totalItem,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize),
                Data = workTicketList
            };

            return workTicketResult;
        }
    }
}
