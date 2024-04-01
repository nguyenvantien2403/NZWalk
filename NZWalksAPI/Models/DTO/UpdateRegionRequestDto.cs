namespace NZWalksAPI.Models.DTO
{
    public class UpdateRegionRequestDto
    {

        //neu khong muon nguoi dung update thuoc tinh nao thi co the bo thuoc tinh do khong cho vao day
        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
