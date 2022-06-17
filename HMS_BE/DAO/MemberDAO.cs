using HMS_BE.DTO;
using HMS_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using AutoMapper;
using System.Linq;

namespace DataAccess
{
    internal class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        private readonly IMapper _mapper;
        private UserDAO()
        {
        }

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }

                    return instance;
                }
            }
        }

        

        public async Task<HMS_BE.DTO.User?> Get(int id)
        {
            var context = new HMSContext();
            HMS_BE.Models.User? User = await context.Users.Where(User => User.Id == id).FirstOrDefaultAsync();
            var r = _mapper.Map<HMS_BE.DTO.User>(User);
            return r;
        }

        public async Task Add(HMS_BE.DTO.User User)
        {
            var context = new HMSContext();
            context.Users.Add(_mapper.Map< HMS_BE.Models.User >( User));
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if ((await Get(id)) != null)
            {
                var context = new HMSContext();
                HMS_BE.Models.User User = new HMS_BE.Models.User() { Id = id };
                context.Users.Attach(User);
                context.Users.Remove(User);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(HMS_BE.DTO.User User)
        {
            var context = new HMSContext();
            context.Users.Update(_mapper.Map<HMS_BE.Models.User>(User));
            await context.SaveChangesAsync();
        }
    }
}
