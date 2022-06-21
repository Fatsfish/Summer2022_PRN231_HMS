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
            var list = await MemberDAO.Instance.Get();
            return _mapper.Map<IEnumerable<HMS_BE.DTO.User>>(list);
        }

        public async Task<HMS_BE.DTO.User> GetUserById(int id)
        {
            var usr = await MemberDAO.Instance.Get(id);
            return _mapper.Map<HMS_BE.DTO.User>(usr);
        }

        public async Task AddUser(HMS_BE.DTO.User user)
        {
            var usr = _mapper.Map<HMS_BE.Models.User>(user);
            await MemberDAO.Instance.Add(usr);
            return;
        }

        public Task UpdateUser(HMS_BE.DTO.User user)
        {
            var usr = _mapper.Map<HMS_BE.DTO.User>(user);
            return MemberDAO.Instance.Update(usr);
        }

        public async Task DeleteUser(int id)
        {
            await MemberDAO.Instance.Delete(id);
            return;
        }

    }
}
