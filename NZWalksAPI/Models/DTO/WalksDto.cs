namespace NZWalksAPI.Models.DTO
{
    public class WalksDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //sau khi them 2 dong cuoi cung thi co the xoa 2 dong ben duoi
        /*public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }*/

        //tao them vi du lieu tra ve bao gom ca region, dung theo thuoc tinh include
        public RegionsDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
