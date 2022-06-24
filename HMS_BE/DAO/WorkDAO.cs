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

        public async Task<HMS_BE.Models.Work?> Get(int id)
        {
            var context = new HMSContext();
            HMS_BE.Models.Work? work = await context.Works.Where(work => work.Id == id).FirstOrDefaultAsync();
            return work;
        }
    }
}
