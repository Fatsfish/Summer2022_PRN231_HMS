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

        public async Task<IEnumerable<HMS_BE.DTO.WorkTicket>> GetAvailableWorkTicketsByUserID(int id)
        {
            var list = await WorkTicketDAO.Instance.GetAvailableWorkTicketsByUserID(id);
            return _mapper.Map<IEnumerable<HMS_BE.DTO.WorkTicket>>(list);
        }

        public async Task<IEnumerable<HMS_BE.DTO.WorkTicket>> GetWorkTicketsByUserID(int id)
        {
            var list = await WorkTicketDAO.Instance.GetWorkTicketsByUserID(id);
            return _mapper.Map<IEnumerable<HMS_BE.DTO.WorkTicket>>(list);
        }
    }
}
