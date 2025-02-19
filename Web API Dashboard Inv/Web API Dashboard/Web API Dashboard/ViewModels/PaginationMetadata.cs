using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API_DASHBOARD.ViewModels
{
    public class PaginationMetadata
    {
        public int totalCount { get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public int startPage { get; set; }
        public int endPage { get; set; }
        public List<PageEntity> pageList { get; set; }
        public PageEntity previousPage { get; set; }
        public PageEntity nextPage { get; set; }
    }

    public class PageEntity
    {
        public int Page { get; set; }
        public string Class { get; set; }
    }
}