using HMS_BE.DTO;
using HMS_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using AutoMapper;
using System.Linq;

namespace HMS_BE.DAO
{
    internal class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();

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

        public async Task<IEnumerable<HMS_BE.Models.User?>> Get()
        {
            var context = new HMSContext();
            List<HMS_BE.Models.User?> user = await context.Users.ToListAsync();
            return user;
        }

        public async Task<HMS_BE.Models.User> Get(int id)
        {
            var context = new HMSContext();
            HMS_BE.Models.User User = await context.Users.Where(User => User.Id == id).FirstOrDefaultAsync();
            return User;
        }

        public async Task<HMS_BE.Models.User> GetUserByEmail(string Email)
        {
            var context = new HMSContext();
            HMS_BE.Models.User User = await context.Users.Where(User => User.Email == Email).FirstOrDefaultAsync();
            return User;
        }

        public async Task Add(HMS_BE.Models.User User)
        {
            var context = new HMSContext();
            context.Users.Add(User);
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

        public async Task Update(HMS_BE.Models.User User)
        {
            var context = new HMSContext();
            context.Users.Update(User);
            await context.SaveChangesAsync();
        }
    }
}
