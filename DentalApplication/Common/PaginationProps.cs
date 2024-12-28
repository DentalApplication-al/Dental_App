namespace DentalApplication.Common
{
    public class PaginationProps : CommandBase
    {
        public PaginationProps(Props? props)
        {
            props = props ?? new();

            page = props.page == null || props.page <= 0 ? 1 : props.page.Value;
            take = props.take == null || props.take <=0 ? 10 : props.take.Value > 50 ? 50 : props.take.Value;
            search = props.search ?? string.Empty;
        }
        public int page { get; set; }
        public int take { get; set; }
        public string? search { get; set; }
    }
}
