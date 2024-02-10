using AutoMapper;
using WalkMe.API.Models.DTO;

namespace WalkMe.API.Mappings;
public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
            // Source and then Destination
            // RegionDto to Region
            // Region to RegionDto
            CreateMap<RegionDto, Region>().ReverseMap();
    }
}
