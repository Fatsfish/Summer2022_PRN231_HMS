﻿using HMS_BE.DTO;
using HMS_BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using AutoMapper;
using System.Linq;

namespace DataAccess
{
    internal class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
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


        public async Task<HMS_BE.Models.User?> Get(int id)
        {
            var context = new HMSContext();
            HMS_BE.Models.User? user = await context.Users.Where(User => User.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task Add(HMS_BE.Models.User user)
        {
            var context = new HMSContext();
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await Get(id);
            if (user != null)
            {
                var context = new HMSContext();
                user.IsDelete = true;
                context.Users.Update(user);
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
