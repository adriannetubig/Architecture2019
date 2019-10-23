using System.Collections.Generic;

namespace BaseData.Entities
{
    public class EPageFilter
    {
        public bool Ascending { get; set; }
        public int ItemsPerPage { get; set; }
        public int PageNo { get; set; }
        public string SortBy { get; set; }
    }
}
