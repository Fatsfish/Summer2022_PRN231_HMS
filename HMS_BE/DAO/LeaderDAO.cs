using HMS_BE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.DAO
{
    public class LeaderDAO
    {
        private static LeaderDAO instance = null;
        private static readonly object instanceLock = new object();

        public static LeaderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LeaderDAO();
                    }

                    return instance;
                }
            }
        }

        public async Task<HMS_BE.Models.Leader?> GetLeaderByGroupId(int id)
        {
            var context = new HMSContext();
            HMS_BE.Models.Leader? leader = await context.Leaders.Where(leader => leader.GroupId == id && leader.IsLeader == true).FirstOrDefaultAsync();
            return leader;
        }
    }
}
