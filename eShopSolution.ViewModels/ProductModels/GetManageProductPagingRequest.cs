using eShopSolution.ViewModels.CommentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.ProductModels
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CateogoryIds { get; set; }
        public string LanguageId { get; set; }
    }
}