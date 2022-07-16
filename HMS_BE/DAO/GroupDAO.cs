using AutoMapper;
using HMS_BE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.DAO
{
    public class GroupDAO
    {
        private static GroupDAO instance = null;
        private static readonly object instanceLock = new object();

        public static GroupDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new GroupDAO();
                    }

                    return instance;
                }
            }
        }
        public async Task<IEnumerable<HMS_BE.Models.Group?>> Get()
        {
            var context = new HMSContext();
            List<HMS_BE.Models.Group?> group = await context.Groups.ToListAsync();
            return group;
        }


        public async Task<HMS_BE.Models.Group?> Get(int id)
        {
            var context = new HMSContext();
            HMS_BE.Models.Group? group = await context.Groups.Where(Group => Group.Id == id).FirstOrDefaultAsync();
            return group;
        }

        public async Task Add(HMS_BE.Models.Group group)
        {
            var context = new HMSContext();
            context.Groups.Add(group);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var group = await Get(id);
            if (group != null)
            {
                var context = new HMSContext();
                group.IsDelete = true;
                context.Groups.Update(group);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(HMS_BE.Models.Group Group)
        {
            var context = new HMSContext();
            context.Groups.Update(Group);
            await context.SaveChangesAsync();
        }

    }
}
