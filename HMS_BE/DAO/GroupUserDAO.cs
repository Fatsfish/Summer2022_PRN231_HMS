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
        private readonly IMapper _mapper;
        private GroupUserDAO()
        {
        }

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

        public async Task<List<HMS_BE.DTO.GroupUser?>> GetGroupUserByGroupId(int id)
        {
            var context = new HMSContext();
            List<HMS_BE.Models.GroupUser?> GroupUsers = await context.GroupUsers.Where(GroupUser => GroupUser.GroupId == id).ToListAsync();
            var r = _mapper.Map<List<HMS_BE.DTO.GroupUser>>(GroupUsers);
            return r;
        }
    }
}
