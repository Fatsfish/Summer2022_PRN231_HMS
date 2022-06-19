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
        private readonly IMapper _mapper;
        private GroupDAO()
        {
        }

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
        public async Task<IEnumerable<HMS_BE.DTO.Group?>> Get()
        {
            var context = new HMSContext();
            List<HMS_BE.Models.Group?> Group = await context.Groups.ToListAsync();
            var r = _mapper.Map<IEnumerable<HMS_BE.DTO.Group>>(Group);
            return r;
        }


        public async Task<HMS_BE.DTO.Group?> Get(int id)
        {
            var context = new HMSContext();
            HMS_BE.Models.Group? Group = await context.Groups.Where(Group => Group.Id == id).FirstOrDefaultAsync();
            var r = _mapper.Map<HMS_BE.DTO.Group>(Group);
            return r;
        }

        public async Task Add(HMS_BE.DTO.Group Group)
        {
            var context = new HMSContext();
            context.Groups.Add(_mapper.Map<HMS_BE.Models.Group>(Group));
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if ((await Get(id)) != null)
            {
                var context = new HMSContext();
                HMS_BE.Models.Group Group = new HMS_BE.Models.Group() { Id = id };
                context.Groups.Attach(Group);
                context.Groups.Remove(Group);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(HMS_BE.DTO.Group Group)
        {
            var context = new HMSContext();
            context.Groups.Update(_mapper.Map<HMS_BE.Models.Group>(Group));
            await context.SaveChangesAsync();
        }

    }
}
