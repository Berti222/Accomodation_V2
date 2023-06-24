using AccomodationModel.Models;
using AccomodationWebAPI.DTOs.GetDTOs;
using AccomodationWebAPI.DTOs.PostDTOs;
using AccomodationWebAPI.DTOs.PutDTOs;
using AutoMapper;

namespace AccomodationWebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateAllergenicMapConfig();
        }

        private void CreateAllergenicMapConfig()
        {
            CreateMap<Allergenic, AllergenicDTO>().ReverseMap();
            CreateMap<AllergenicPostDTO, Allergenic>();
            CreateMap<AllergenicPutDTO, Allergenic>();
        }
    }
}
