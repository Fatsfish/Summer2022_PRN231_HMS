using AutoMapper;
using HMS_BE.DAO;
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
        public async Task<IEnumerable<HMS_BE.DTO.User>> GetUserList()
        {
            var list = await UserDAO.Instance.Get();
            return _mapper.Map<IEnumerable<HMS_BE.DTO.User>>(list);
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
