namespace AccomodationWebAPI.DTOs
{
    public class PagingDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
    }
}
