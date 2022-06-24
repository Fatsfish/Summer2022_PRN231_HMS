using AutoMapper;
using HMS_BE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.DAO
{
    public class GroupUserDAO
    {
        private static GroupUserDAO instance = null;
        private static readonly object instanceLock = new object();

        public static GroupUserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new GroupUserDAO();
                    }

                    return instance;
                }
            }
        }

        public async Task<IEnumerable<HMS_BE.Models.GroupUser?>> GetGroupUserByGroupId(int id)
        {
            var context = new HMSContext();
            List<HMS_BE.Models.GroupUser?> groupUsers = await context.GroupUsers.Where(GroupUser => GroupUser.GroupId == id).ToListAsync();
            return groupUsers;
        }

        public async Task<HMS_BE.Models.GroupUser?> GetGroupUserByID(int id)
        {
            var context = new HMSContext();
            HMS_BE.Models.GroupUser? groupUser = await context.GroupUsers.Where(GroupUser => GroupUser.Id == id).FirstOrDefaultAsync();
            return groupUser;
        }

        public async Task Delete(int id)
        {
            if ((await GetGroupUserByGroupId(id)) != null)
            {
                var context = new HMSContext();
                HMS_BE.Models.GroupUser GroupUser = new HMS_BE.Models.GroupUser() { Id = id };
                context.GroupUsers.Attach(GroupUser);
                context.GroupUsers.Remove(GroupUser);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<HMS_BE.Models.GroupUser?>> GetConditionGroupUserByGroupId(int? id, bool condition)
        {
            var context = new HMSContext();
            List<HMS_BE.Models.GroupUser?> groupUsers;
            if (id == null)
            {
                groupUsers = await context.GroupUsers.Where(GroupUser =>GroupUser.IsLeader == condition).ToListAsync();
            }
            else
            {
                groupUsers = await context.GroupUsers.Where(GroupUser => GroupUser.GroupId == id && GroupUser.IsLeader == condition).ToListAsync();
            }
            
            return groupUsers;
        }

        public async Task Add(HMS_BE.Models.GroupUser groupUser)
        {
            groupUser.IsLeader = false;
            var context = new HMSContext();
            context.GroupUsers.Add(groupUser);
            await context.SaveChangesAsync();
        }

        public async Task Update(HMS_BE.Models.GroupUser groupUser)
        {
            var context = new HMSContext();
            context.GroupUsers.Update(groupUser);
            await context.SaveChangesAsync();
        }

        public async Task AddList(IEnumerable<HMS_BE.Models.GroupUser> groupUsers)
        {
            var context = new HMSContext();
            await context.GroupUsers.AddRangeAsync(groupUsers);
            await context.SaveChangesAsync();
        }
    }
}
