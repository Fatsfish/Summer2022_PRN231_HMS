using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface IAllowedWorkGroupRepository
    {
        Task<BasePagingModel<HMS_BE.DTO.AllowedWorkGroupModel>> GetAllowedWorkGroupsByGroupID(AllowedWorkGroupSearchModel searchModel , PagingModel paging);
    }
}
