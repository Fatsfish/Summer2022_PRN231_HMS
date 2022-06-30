using HMS_BE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.DAO
{
    public class WorkDAO
    {
        private static WorkDAO instance = null;
        private static readonly object instanceLock = new object();

        public static WorkDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new WorkDAO();
                    }

                    return instance;
                }
            }
        }

        public async Task<IEnumerable<HMS_BE.Models.Work?>> Get()
        {
            var context = new HMSContext();
            List<HMS_BE.Models.Work?> work = await context.Works.ToListAsync();
            return work;
        }

        public async Task<IEnumerable<HMS_BE.Models.Work?>> GetAvailableWork()
        {
            var context = new HMSContext();
            List<HMS_BE.Models.Work?> work = await context.Works.Where(work => work.IsDelete == false).ToListAsync();
            return work;
        }


        public async Task<HMS_BE.Models.Work?> Get(int id)
        {
            var context = new HMSContext();
            HMS_BE.Models.Work? work = await context.Works.Where(work => work.Id == id).FirstOrDefaultAsync();
            return work;
        }

        public async Task Add(HMS_BE.Models.Work work)
        {
            var context = new HMSContext();
            context.Works.Add(work);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var work = await Get(id);
            if (work != null)
            {
                var context = new HMSContext();
                work.IsDelete = true;
                context.Works.Update(work);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(HMS_BE.Models.Work Work)
        {
            var context = new HMSContext();
            var tmpWork = Get(Work.Id);
            if(tmpWork == null)
            {
                context.Works.Update(Work);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<HMS_BE.Models.Work?>> GetWorkById(int id)
        {
            var context = new HMSContext();
            IEnumerable<HMS_BE.Models.Work?> works = await context.Works.Where(Work => Work.Id == id).ToListAsync();
            return works;
        }

    }
}
