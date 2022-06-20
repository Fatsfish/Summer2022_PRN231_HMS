using AutoMapper;
using HMS_BE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.DAO
{
    public class WorkTicketDAO
    {
        private static WorkTicketDAO instance = null;
        private static readonly object instanceLock = new object();
        private readonly IMapper _mapper;
        private WorkTicketDAO()
        {
        }

        public static WorkTicketDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new WorkTicketDAO();
                    }

                    return instance;
                }
            }
        }

        public async Task<IEnumerable<HMS_BE.DTO.WorkTicket?>> GetAvailableWorkTicketsByUserID(int id)
        {
            var context = new HMSContext();
            List<HMS_BE.Models.WorkTicket?> workTickets = await context.WorkTickets.Where(wt => wt.OwnerId == id && wt.IsDelete == false).ToListAsync();
            var r = _mapper.Map<IEnumerable<HMS_BE.DTO.WorkTicket>>(workTickets);
            return r;
        }

        public async Task<IEnumerable<HMS_BE.DTO.WorkTicket?>> GetWorkTicketsByUserID(int id)
        {
            var context = new HMSContext();
            List<HMS_BE.Models.WorkTicket?> workTickets = await context.WorkTickets.Where(wt => wt.OwnerId == id).ToListAsync();
            var r = _mapper.Map<IEnumerable<HMS_BE.DTO.WorkTicket>>(workTickets);
            return r;
        }

        public async Task<bool> CanLeaveGroup(int id)
        {
            var context = new HMSContext();
            List<HMS_BE.Models.WorkTicket?> workTickets = await context.WorkTickets.Where(wt => wt.OwnerId == id && wt.IsDelete == false && wt.Status != "Completed").ToListAsync();
            var r = workTickets.Count > 0 ? false : true;
            return r;
        }
    }
}
