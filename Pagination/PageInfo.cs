#nullable disable

namespace FM_API.Models
{
    public class PageInfo
    {
        public PageInfo()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PageInfo(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }

        public int Skip => (PageNumber - 1) * PageSize;
    }
}
