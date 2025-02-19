using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API_DASHBOARD.ViewModels
{
    public class PagingHeaders
    {
        // https://jasonwatmore.com/post/2015/10/30/aspnet-mvc-pagination-example-with-logic-like-google

        public PaginationMetadata Create(int TotalPages, int CurrentPage, int TotalCount, int PageSize)
        {
            List<PageEntity> pageList = new List<PageEntity>();
            for (int i = 0; i < TotalPages; i++)
            {
                int page = i + 1;
                if (page == CurrentPage)
                {
                    pageList.Add(new PageEntity { Class = "active", Page = page });
                }
                else
                {
                    pageList.Add(new PageEntity { Class = "", Page = page });
                }
            }

            PageEntity previousPage = new PageEntity();
            if (CurrentPage > 1)
            {
                previousPage.Class = "enabled";
                previousPage.Page = CurrentPage - 1;
            }
            else
            {
                previousPage.Class = "disabled";
                previousPage.Page = CurrentPage;
            }

            PageEntity nextPage = new PageEntity();
            if (CurrentPage < TotalPages)
            {
                nextPage.Class = "enabled";
                nextPage.Page = CurrentPage + 1;
            }
            else
            {
                nextPage.Class = "disabled";
                nextPage.Page = CurrentPage;
            }

            var startPage = CurrentPage - 5;
            var endPage = CurrentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > TotalPages)
            {
                endPage = TotalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            PaginationMetadata paginationMetadata = new PaginationMetadata();
            paginationMetadata.totalCount = TotalCount;
            paginationMetadata.totalPages = TotalPages;
            paginationMetadata.pageSize = PageSize;
            paginationMetadata.currentPage = CurrentPage;
            paginationMetadata.pageList = pageList;
            paginationMetadata.previousPage = previousPage;
            paginationMetadata.nextPage = nextPage;
            paginationMetadata.startPage = startPage;
            paginationMetadata.endPage = endPage;
            return paginationMetadata;
        }
    }
}