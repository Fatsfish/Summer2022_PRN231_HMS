using AutoMapper;
using BusinessLayer.ResponseModels.ViewModels;
using HMS_BE.DAO;
using HMS_BE.Models.PagingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<BasePagingModel<HMS_BE.DTO.User>> GetUserList(PagingModel paging)
        {
            var list = await UserDAO.Instance.Get();

            // Calculate total item and total page
            int totalItem = list.ToList().Count;

            list = list.Skip((paging.PageIndex - 1) * paging.PageSize)
                .Take(paging.PageSize).ToList();

            var userResult = new BasePagingModel<HMS_BE.DTO.User>()
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalItem = totalItem,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize)
            };

            return userResult;
        }

        public async Task<HMS_BE.DTO.User> GetUserById(int id)
        {
            var usr = await UserDAO.Instance.Get(id);
            return _mapper.Map<HMS_BE.DTO.User>(usr);
        }

        public async Task AddUser(HMS_BE.DTO.User user)
        {
            var usr = _mapper.Map<HMS_BE.DTO.User>(user);
            await UserDAO.Instance.Add(usr);
            return;
        }

        public Task UpdateUser(HMS_BE.DTO.User user)
        {
            var usr = _mapper.Map<HMS_BE.DTO.User >(user);
            return UserDAO.Instance.Update(usr);
        }

        public async Task DeleteUser(int id)
        {
            await UserDAO.Instance.Delete(id);
            return;
        }

    }
}
