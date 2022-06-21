﻿using AutoMapper;
using HMS_BE.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public class GroupUserRepository : IGroupUserRepository
    {
        private readonly IMapper _mapper;

        public GroupUserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task AddGroupUser(HMS_BE.DTO.GroupUser groupUser)
        {
            var gr = _mapper.Map<HMS_BE.Models.GroupUser>(groupUser);
            await GroupUserDAO.Instance.Add(gr);
            return;
        }

        public async Task<IEnumerable<HMS_BE.DTO.GroupUser>> GetConditionGroupUsersByGroupId(int id, bool condition)
        {
            var list = await GroupUserDAO.Instance.GetConditionGroupUserByGroupId(id, condition);
            return _mapper.Map<IEnumerable<HMS_BE.DTO.GroupUser>>(list);
        }

        public async Task<HMS_BE.DTO.GroupUser> GetGroupUserByID(int id)
        {
            var groupuser = await GroupUserDAO.Instance.GetGroupUserByID(id);
            return _mapper.Map<HMS_BE.DTO.GroupUser>(groupuser);
        }

        public async Task<IEnumerable<HMS_BE.DTO.GroupUser>> GetGroupUsersByGroupId(int id)
        {
            var list = await GroupUserDAO.Instance.GetGroupUserByGroupId(id);
            return _mapper.Map<IEnumerable<HMS_BE.DTO.GroupUser>>(list);
        }

        public Task RemoveGroupUser(int id)
        {
            return GroupUserDAO.Instance.Delete(id);
        }
    }
}