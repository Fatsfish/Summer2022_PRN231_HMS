﻿using HMS_BE.Constants;
using HMS_BE.DTO.PagingModel;

namespace HMS_BE.Utils
{
    public static class PagingUtil
    {
        public static PagingModel getDefaultPaging()
        {
            return new PagingModel
            {
                PageIndex = PageConstant.DefaultPageIndex,
                PageSize = PageConstant.DefaultPageSize
            };
        }
        public static PagingModel checkDefaultPaging(PagingModel paging)
        {
            if (paging.PageIndex <= 0) paging.PageIndex = PageConstant.DefaultPageIndex;
            if (paging.PageSize <= 0) paging.PageSize = PageConstant.DefaultPageSize;
            return paging;
        }
    }
}
