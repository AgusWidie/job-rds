using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API_DASHBOARD.ViewModels
{
    public class PaginationFilter
    {
        const int maxPageSize = 100;
        public int pageNumber { get; set; } = 1;
        public int _pageSize { get; set; } = 5;

        public int pageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string searchString { get; set; }
        public string searchString2 { get; set; }
        public string filterString { get; set; }
        public string filterString2 { get; set; }
        public string filterString3 { get; set; }
        public string filterString4 { get; set; }
        public string filterString5 { get; set; }
        public string filterString6 { get; set; }
        public string filterString7 { get; set; }
        public string filterString8 { get; set; }
        public string filterString9 { get; set; }
        public string filterString10 { get; set; }
        public string filterString11 { get; set; }
        public string filterString12 { get; set; }
        public string filterString13 { get; set; }
        public string filterString14 { get; set; }
        public string filterString15 { get; set; }
        public int? filterStatus { get; set; }
        public string createdAt { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public bool bolFlag { get; set; }
    }
}