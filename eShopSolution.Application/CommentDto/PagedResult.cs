using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.CommentDto
{
    public class PagedResult<T>
    {
        List<T> Item { get; set; }
        public int TotaRecord { get; set; }
    }
}
