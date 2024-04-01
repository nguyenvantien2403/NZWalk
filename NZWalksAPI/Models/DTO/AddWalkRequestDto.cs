using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Models.DTO
{
    //DTO la nhan du lieu tu khach hang 
    public class AddWalkRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }


       
    }
}
