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
        public Task<IEnumerable<WorkTicket>> GetWorkTicketsByUserID(int id)
        {
            return WorkTicketDAO.Instance.GetWorkTicketsByUserID(id);
        }
    }
}
