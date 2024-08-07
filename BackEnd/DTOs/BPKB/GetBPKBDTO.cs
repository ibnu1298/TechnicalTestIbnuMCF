namespace BackEnd.DTOs.BPKB
{
    public class GetBPKBDTO
    {
        public string AgreementNumber { get; set; }
        public string? BpkbNo { get; set; }
        public string? BranchId { get; set; }
        public DateTime BpkbDate { get; set; }
        public string? FakturNo { get; set; }
        public DateTime FakturDate { get; set; }
        public string? LocationId { get; set; }
        public string? PoliceNo { get; set; }
        public DateTime BpkbDateIn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
    public class ListGetBPKBDTO : BaseResponse
    {
        public IEnumerable<GetBPKBDTO>? ListBPKB { get; set; }
    }
    public class objGetBPKBDTO : BaseResponse
    {
        public GetBPKBDTO BPKB { get; set; }
    }

}
