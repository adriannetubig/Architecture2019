namespace BaseModel
{
    public class PageFilter
    {
        public int Offset => (PageNo - 1) * ItemsPerPage;
        public int PageNo { get; set; }
        public int ItemsPerPage { get; set; }
    }
    public class PageFilter<T> : PageFilter
    {
        public T Filter { get; set; }
    }
}
