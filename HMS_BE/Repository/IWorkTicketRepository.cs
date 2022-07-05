using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface IWorkTicketRepository
    {
        Task<IEnumerable<HMS_BE.DTO.WorkTicket>> GetWorkTicketsByUserID(int id);
        Task<IEnumerable<HMS_BE.DTO.WorkTicket>> GetAvailableWorkTicketsByUserID(int id);
        Task<IEnumerable<HMS_BE.DTO.WorkTicket>> GetDoneWorkTickets();
        Task<BasePagingModel<HMS_BE.DTO.WorkTicketModel>> GetWorkTickets(WorkTicketSearchModel searchModel, PagingModel paging);
        Task<bool> CanLeaveGroup(int id);
        Task AddWorkTicket(HMS_BE.DTO.WorkTicket workTicket);
        Task UpdateWorkTicket(HMS_BE.DTO.WorkTicket workTicket);
        Task DeleteWorkTicket(int id);
    }
}
