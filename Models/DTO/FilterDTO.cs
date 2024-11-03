namespace MadhurAPI.Models.DTO
{
    public class FilterDTO
    {
        public string? Name { get; set; }
        public string? Mobile { get; set; }
        public string? Param { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
