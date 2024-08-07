namespace BackEnd.DTOs.Location
{
    public class GetLocation
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
    }
    public class ListGetLocation : BaseResponse
    {
        public IEnumerable<GetLocation> ListStorageLocation { get; set; }
    }
}
