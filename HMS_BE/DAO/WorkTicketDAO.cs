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

        public async Task<IEnumerable<HMS_BE.Models.WorkTicket?>> GetAvailableWorkTicketsByUserID(int id)
        {
            var context = new HMSContext();
            List<HMS_BE.Models.WorkTicket?> workTickets = await context.WorkTickets.Where(wt => wt.OwnerId == id && wt.IsDelete == false).ToListAsync();
            return workTickets;
        }

        public async Task<IEnumerable<HMS_BE.Models.WorkTicket?>> GetWorkTicketsByUserID(int id)
        {
            var context = new HMSContext();
            List<HMS_BE.Models.WorkTicket?> workTickets = await context.WorkTickets.Where(wt => wt.OwnerId == id).ToListAsync();
            return workTickets;
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
