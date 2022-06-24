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
        Task<HMS_BE.DTO.Work> GetWorkById(int id);
        Task AddWork(HMS_BE.DTO.Work work);
        Task UpdateWork(HMS_BE.DTO.Work work);
        Task DeleteWork(int id);
    }
}
