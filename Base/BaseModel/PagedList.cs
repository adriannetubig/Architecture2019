using System.Collections.Generic;

namespace BaseModel
{
    public class PagedList<T>
    {
        public int NumberOfPages => (NumberOfItems < ItemsPerPage ? 1 : NumberOfItems / ItemsPerPage);
        public int ItemsPerPage { get; set; }
        public int NumberOfItems { get; set; }
        public int PageNo { get; set; }
        public List<T> Items { get; set; }
    }
}
