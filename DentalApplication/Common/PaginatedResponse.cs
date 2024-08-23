namespace DentalApplication.Common
{
    public class PaginatedResponse<T>
    {
        public List<T> data { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public long totalElements { get; set; }
        public int totalPages { get; set; }
    }
}
