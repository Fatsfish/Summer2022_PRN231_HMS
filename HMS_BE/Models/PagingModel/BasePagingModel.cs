﻿using System.Collections.Generic;

namespace HMS_BE.Models.PagingModel
{
    public class BasePagingModel<TViewModel>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItem { get; set; }
        public int TotalPage { get; set; }

        public List<TViewModel> Data { get; set; }
    }
}
