using eShopSolution.ViewModels.CommentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}