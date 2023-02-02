using eShopSolution.ViewModels.CommentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.ProductModels
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
