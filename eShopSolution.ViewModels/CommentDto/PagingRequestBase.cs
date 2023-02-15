using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.CommentDto
{
    public class PagingRequestBase : RequestBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}