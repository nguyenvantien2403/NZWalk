using AutoMapper;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mappings
{
    //mapping giup code ngan gon hon trong viec anh xa giua dto va domain model
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Region, RegionsDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto,Walk>().ReverseMap();
            CreateMap<Walk,WalksDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();




        }
        public class userDTO
        {
            public string fullName { get; set; }
        }
        public class userDomain
        {
            public string fullName { get; set; }

        }
    }
}
