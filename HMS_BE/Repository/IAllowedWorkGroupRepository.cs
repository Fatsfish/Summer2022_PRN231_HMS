using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface IAllowedWorkGroupRepository
    {
        Task<IEnumerable<HMS_BE.DTO.AllowedWorkGroup>> GetAllowedWorkGroupsByGroupID(int id);
    }
}
