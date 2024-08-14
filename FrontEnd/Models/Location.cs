namespace FrontEnd.Models
{
   
    public class Location
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
    }
    public class ListLocation : BaseResponse
    {
        public IEnumerable<Location> ListStorageLocation { get; set; }
    }
}
