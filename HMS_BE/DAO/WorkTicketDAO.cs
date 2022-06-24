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

        public async Task<IEnumerable<HMS_BE.Models.WorkTicket?>> GetDoneWorkTicket()
        {
            var context = new HMSContext();
            List<HMS_BE.Models.WorkTicket?> work = await context.WorkTickets.Where(work => work.IsDelete == false).ToListAsync();
            return work;
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

        public async Task<HMS_BE.Models.WorkTicket?> Get(int id)
        {
            var context = new HMSContext();
            HMS_BE.Models.WorkTicket? workTicket = await context.WorkTickets.Where(WorkTicket => WorkTicket.Id == id).FirstOrDefaultAsync();
            return workTicket;
        }

        public async Task Add(HMS_BE.Models.WorkTicket workTicket)
        {
            var context = new HMSContext();
            context.WorkTickets.Add(workTicket);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var workTicket = await Get(id);
            if (workTicket != null)
            {
                var context = new HMSContext();
                workTicket.IsDelete = true;
                context.WorkTickets.Update(workTicket);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(HMS_BE.Models.WorkTicket WorkTicket)
        {
            var context = new HMSContext();
            var tmpWork = Get(WorkTicket.Id);
            if (tmpWork == null)
            {
                context.WorkTickets.Update(WorkTicket);
                await context.SaveChangesAsync();
            }
        }
    }
}
