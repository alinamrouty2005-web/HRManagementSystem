using System;
using System.Collections.Generic;
using System.Text;

namespace HRManagement.Core.DTOs
{
    public class PagedResultDto<T>
    {
        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public List<T> Data { get; set; } = new List<T>();
    }
}
