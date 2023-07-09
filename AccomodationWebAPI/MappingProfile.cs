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
            CreateRoomPriceMapConfig();
        }

        private void CreateAllergenicMapConfig()
        {
            CreateMap<Allergenic, AllergenicDTO>().ReverseMap();
            CreateMap<AllergenicPostDTO, Allergenic>();
            CreateMap<AllergenicPutDTO, Allergenic>();
        }

        private void CreateRoomPriceMapConfig()
        {
            CreateMap<RoomPrice, RoomPriceDTO>().ReverseMap();
            CreateMap<RoomPricePostDTO, RoomPrice>();
            CreateMap<RoomPricePutDTO, RoomPrice>();
        }
    }
}
