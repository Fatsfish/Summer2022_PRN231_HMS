﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Repository
{
    public interface IWorkRepository
    {
        Task<HMS_BE.DTO.Work> GetWorkById(int id);
    }
}
