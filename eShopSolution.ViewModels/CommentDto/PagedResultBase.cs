using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.CommentDto
{
    public class PagedResultBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public int TotaRecords { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)TotaRecords / PageSize;
                return (int)Math.Ceiling(pageCount); // Ceiling hàm làm tròn lên.
            }
        }
    }
}