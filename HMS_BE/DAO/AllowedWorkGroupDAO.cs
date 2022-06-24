using HMS_BE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.DAO
{
    public class AllowedWorkGroupDAO
    {
        private static AllowedWorkGroupDAO instance = null;
        private static readonly object instanceLock = new object();

        public static AllowedWorkGroupDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AllowedWorkGroupDAO();
                    }

                    return instance;
                }
            }
        }

        public async Task<IEnumerable<HMS_BE.Models.AllowedWorkGroup?>> GetAllowedWorkGroupByGroupId(int id)
        {
            var context = new HMSContext();
            IEnumerable<HMS_BE.Models.AllowedWorkGroup?> workgroups = await context.AllowedWorkGroups.Where(Group => Group.Id == id).ToListAsync();
            return workgroups;
        }
    }
}
