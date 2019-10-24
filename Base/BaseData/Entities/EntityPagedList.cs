using System.Collections.Generic;

namespace BaseData.Entities
{
    public class EntityPagedList<T> where T : class
    {
        public int ItemsPerPage { get; set; }
        public int PageNo { get; set; }
        public int NumberOfItems { get; set; }
        public List<T> Items { get; set; }
    }
}
