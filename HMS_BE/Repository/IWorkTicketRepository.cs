using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface IWorkTicketRepository
    {
        Task<IEnumerable<HMS_BE.DTO.WorkTicket>> GetWorkTicketsByUserID(int id);
    }
}
