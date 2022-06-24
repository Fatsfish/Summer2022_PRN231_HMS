using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface ILeaderRepository
    {
        Task<HMS_BE.DTO.Leader> GetLeaderByGroupId(int id);
    }
}
