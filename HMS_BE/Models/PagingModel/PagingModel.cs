using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_BE.Models;

public class PagingModel
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
