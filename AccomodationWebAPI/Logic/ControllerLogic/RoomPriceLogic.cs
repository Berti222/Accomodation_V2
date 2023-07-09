using AccomodationModel.AccomodationRepository;
using AccomodationModel.Models;
using AccomodationWebAPI.CustomExceptions;
using AccomodationWebAPI.DTOs;
using AccomodationWebAPI.DTOs.GetDTOs;
using AccomodationWebAPI.DTOs.PostDTOs;
using AccomodationWebAPI.DTOs.PutDTOs;
using AccomodationWebAPI.Logic.Factories;
using AccomodationWebAPI.Logic.Helpers;
using AutoMapper;
using System.Net;

namespace AccomodationWebAPI.Logic.ControllerLogic
{
    public class RoomPriceLogic : ControllerLogicBase<RoomPriceDTO, RoomPricePostDTO, RoomPricePutDTO>
    {
        public RoomPriceLogic(IUnitOfWork unitOfWork, IMapper mapper, IPagingFactroy pagingFactroy)
            : base(unitOfWork, mapper, pagingFactroy)
        {
        }

        public override async Task<RoomPriceDTO> CreateAsync(RoomPricePostDTO entity)
        {
            try
            {
                RoomPrice entityForCreate = mapper.Map<RoomPricePostDTO, RoomPrice>(entity);

                await unitOfWork.RoomPriceRepository.CreateAsync(entityForCreate);
                await unitOfWork.SaveAsync();

                return mapper.Map<RoomPrice, RoomPriceDTO>(entityForCreate);
            }
            catch (Exception ex)
            {
                throw new HTTPStatusException(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public override async Task DeleteAsync(int id)
        {
            var entityInDb = await GetByIdAsync(id);

            if (entityInDb is null) throw new HTTPStatusException("Entiy not exists in DB.", HttpStatusCode.NotFound);

            unitOfWork.RoomPriceRepository.Delete(entityInDb);
            await unitOfWork.SaveAsync();
        }

        public override async Task<PagingDTO<RoomPriceDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entitiesInDb = await unitOfWork.RoomPriceRepository.GetAllAsync(tracked: false);
            if (entitiesInDb == null || entitiesInDb.Count() == 0) throw new HTTPStatusException("No etities found.", HttpStatusCode.NotFound);

            var mappedEntities = mapper.Map<List<RoomPriceDTO>>(entitiesInDb);

            var page = pagingFactroy.Create(mappedEntities, pageNumber, pageSize);
            return page;
        }

        public override async Task<RoomPriceDTO> GetAsync(int id)
        {
            var entityFoundInDB = await GetByIdAsync(id);

            if (entityFoundInDB is null) throw new HTTPStatusException("Not found.", HttpStatusCode.NotFound);

            return mapper.Map<RoomPriceDTO>(entityFoundInDB);
        }

        // Just the latest Item could be updated wich connects to roomtype
        public override async Task<RoomPriceDTO> UpdateAsync(RoomPricePutDTO entity)
        {
            if (entity is null) throw new HTTPStatusException("Entity is null", HttpStatusCode.BadRequest);

            var fromDto = mapper.Map<RoomPrice>(entity);
            var entityInDb = await GetCurrentByRoomTypeId(entity.RoomTypeId);
            if (entityInDb is null) throw new HTTPStatusException("Entity with the id not exist in DB.", HttpStatusCode.NotFound);

            SetFieldsForUpdate(entityInDb, fromDto);

            await unitOfWork.RoomPriceRepository.UpdateAsync(entityInDb);
            await unitOfWork.SaveAsync();

            return mapper.Map<RoomPriceDTO>(entityInDb);
        }

        private async Task<RoomPrice> GetByIdAsync(int id)
            => await unitOfWork.RoomPriceRepository.GetByConditionAsync(rp => rp.Id == id);

        private async Task<RoomPrice> GetCurrentByRoomTypeId(int roomTypeId)
            => await unitOfWork.RoomPriceRepository.GetByConditionAsync(rp => rp.RoomTypeId == roomTypeId && rp.PeriodEnd == null);
    }
}
