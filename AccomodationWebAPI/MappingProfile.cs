using AccomodationModel.Models;
using AccomodationWebAPI.AutoMapper;
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
            CreateRoomTypeMapConfig();
            CreateEquipmentMapConfig();
            CreateRoomMapConfig();
            CreateGuestMapConfig();
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

        private void CreateRoomTypeMapConfig()
        {
            CreateMap<RoomType, RoomTypeDTO>().ForMember(dest => dest.Price, 
                                                        opt => opt.MapFrom(new RoomTypeDTOResolver())).ReverseMap();
            CreateMap<RoomTypePostDTO, RoomType>();
            CreateMap<RoomTypePutDTO, RoomType>();
        }

        private void CreateEquipmentMapConfig()
        {
            CreateMap<Equipment, EquipmentDTO>().ReverseMap();
            CreateMap<EquipmentPostDTO, Equipment>();
            CreateMap<EquipmentPutDTO, Equipment>();
        }

        private void CreateRoomMapConfig()
        {
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<RoomPostDTO, Room>();
            CreateMap<RoomPutDTO, Room>();
        }

        private void CreateGuestMapConfig()
        {
            CreateMap<Guest, GuestDTO>().ReverseMap();
            CreateMap<GuestPostDTO, Guest>();
            CreateMap<GuestPutDTO, Guest>();
        }
    }
}
