using AutoMapper;
using HMS_BE.DAO;
using HMS_BE.DTO;
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

        public async Task DeleteWorkTicket(int id)
        {
            await WorkTicketDAO.Instance.Delete(id);
        }

        public async Task<IEnumerable<WorkTicket>> GetAvailableWorkTicketsByUserID(int id)
        {
            var workTicketList = await WorkTicketDAO.Instance.GetAvailableWorkTicketsByUserID(id);
            var data = _mapper.Map<IEnumerable<Models.WorkTicket>, IEnumerable<WorkTicket>>(workTicketList);
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
    }
}
