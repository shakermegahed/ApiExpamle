
namespace Entity
{
    public class PagedQueryParameters
    {
        public PagedQueryParameters(int pageSize, int pageNumber, string keyword = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Keyword = keyword;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
    }
}
