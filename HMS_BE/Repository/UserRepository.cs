using AutoMapper;
using HMS_BE.DAO;
using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace HMS_BE.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<BasePagingModel<HMS_BE.DTO.User>> GetUserList(UserSearchModel searchModel, PagingModel paging)
        {
            var list = await UserDAO.Instance.Get();

            // Converting from IEnumerable to List
            List<HMS_BE.DTO.User> usersList = _mapper.Map<IEnumerable<HMS_BE.DTO.User>>(list).ToList();

            // Search for user list
            usersList = usersList.Where(x => StringNormalizer.VietnameseNormalize(x.FirstName + ' ' + x.LastName)
                            .Contains(StringNormalizer.VietnameseNormalize(searchModel.SearchTerm)))
                        .Where(x => (searchModel.isActive != null) ? x.IsActive == (bool)searchModel.isActive
                                            : true)
                        .ToList();

            // Calculate total item
            int totalItem = usersList.ToList().Count;

            usersList = usersList.Skip((paging.PageIndex - 1) * paging.PageSize)
                .Take(paging.PageSize).ToList();

            var userResult = new BasePagingModel<HMS_BE.DTO.User>()
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalItem = totalItem,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize),
                Data = usersList
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
            var usr = _mapper.Map<HMS_BE.Models.User>(user);
            await UserDAO.Instance.Add(usr);
            return;
        }

        public Task UpdateUser(HMS_BE.DTO.User user)
        {
            var usr = _mapper.Map<HMS_BE.Models.User >(user);
            return UserDAO.Instance.Update(usr);
        }

        public async Task DeleteUser(int id)
        {
            await UserDAO.Instance.Delete(id);
            return;
        }

    }
}
