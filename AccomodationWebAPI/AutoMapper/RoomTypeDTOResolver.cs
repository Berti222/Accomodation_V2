using AccomodationModel.Models;
using AccomodationWebAPI.DTOs.GetDTOs;
using AutoMapper;

namespace AccomodationWebAPI.AutoMapper
{
    public class RoomTypeDTOResolver : IValueResolver<RoomType, RoomTypeDTO, RoomPriceDTO>
    {
        public RoomPriceDTO Resolve(RoomType source, RoomTypeDTO destination, RoomPriceDTO destMember, ResolutionContext context)
        {
            var roomPrices = source.RoomPrices.FirstOrDefault(rp => rp.PeriodEnd == null);
            var mappedInstance = context.Mapper.Map<RoomPrice, RoomPriceDTO>(roomPrices);
            return mappedInstance;
        }
    }
}
