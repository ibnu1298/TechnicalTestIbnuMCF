namespace BackEnd.Models
{
    public class MsStorageLocation
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public virtual ICollection<TrBpkb> TrBpkbs { get; set; } = new List<TrBpkb>();
    }
}
