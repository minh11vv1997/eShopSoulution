using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.CommentDto
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Item { get; set; }
    }
}