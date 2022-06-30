using HMS_BE.DTO.PagingModel;
using HMS_BE.DTO.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface IWorkRepository
    {
        Task<IEnumerable<HMS_BE.DTO.Work>> GetWorkList();
        Task<IEnumerable<HMS_BE.DTO.Work>> GetAvalableWorkList();
        Task<BasePagingModel<HMS_BE.DTO.WorkModel>> GetWorkById(WorkSearchModel searchModel, PagingModel paging);
        Task AddWork(HMS_BE.DTO.Work work);
        Task UpdateWork(HMS_BE.DTO.Work work);
        Task DeleteWork(int id);
    }
}
