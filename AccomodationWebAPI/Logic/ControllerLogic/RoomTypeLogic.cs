using AccomodationModel.AccomodationRepository;
using AccomodationModel.Models;
using AccomodationWebAPI.CustomExceptions;
using AccomodationWebAPI.DTOs;
using AccomodationWebAPI.DTOs.GetDTOs;
using AccomodationWebAPI.DTOs.PostDTOs;
using AccomodationWebAPI.DTOs.PutDTOs;
using AccomodationWebAPI.Logic.Factories;
using AutoMapper;
using System.Net;

namespace AccomodationWebAPI.Logic.ControllerLogic
{
    public class RoomTypeLogic : ControllerLogicBase<RoomTypeDTO, RoomTypePostDTO, RoomTypePutDTO>
    {
        public RoomTypeLogic(IUnitOfWork unitOfWork, IMapper mapper, IPagingFactroy pagingFactroy)
            :base(unitOfWork, mapper, pagingFactroy)
        {
        }

        public override async Task<RoomTypeDTO> CreateAsync(RoomTypePostDTO entity)
        {
            var enityWithTheSameName = await unitOfWork.RoomTypeRepository.GetByConditionAsync(rt => rt.Name == entity.Name);

            if (enityWithTheSameName is not null) throw new HTTPStatusException("Entiy with the same name already exists.", HttpStatusCode.BadRequest);

            try
            {
                RoomType entityForCreate = mapper.Map<RoomTypePostDTO, RoomType>(entity);
                await unitOfWork.RoomTypeRepository.CreateAsync(entityForCreate);
                await unitOfWork.SaveAsync();

                return mapper.Map<RoomType, RoomTypeDTO>(entityForCreate);
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

            unitOfWork.RoomTypeRepository.Delete(entityInDb);
            await unitOfWork.SaveAsync();
        }

        public override async Task<PagingDTO<RoomTypeDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entitiesInDb = await unitOfWork.RoomTypeRepository.GetAllAsync(tracked: false, includeProperties: "RoomPrices");
            if (entitiesInDb == null || entitiesInDb.Count() == 0) throw new HTTPStatusException("No etities found.", HttpStatusCode.NotFound);

            var mappedEntities = mapper.Map<List<RoomTypeDTO>>(entitiesInDb);

            var page = pagingFactroy.Create(mappedEntities, pageNumber, pageSize);
            return page;
        }

        public override async Task<RoomTypeDTO> GetAsync(int id)
        {
            var entityFoundInDB = await unitOfWork.RoomTypeRepository.GetByConditionAsync(rt => rt.Id == id, includeProperties: "RoomPrices");

            if (entityFoundInDB is null) throw new HTTPStatusException("Not found.", HttpStatusCode.NotFound);

            return mapper.Map<RoomTypeDTO>(entityFoundInDB);
        }

        public override async Task<RoomTypeDTO> UpdateAsync(RoomTypePutDTO entity)
        {
            if (entity is null) throw new HTTPStatusException("Entity is null", HttpStatusCode.BadRequest);

            var fromDto = mapper.Map<RoomType>(entity);
            var entityInDb = await GetByIdAsync(entity.Id);
            if (entityInDb is null) throw new HTTPStatusException("Entity with the id not exist in DB.", HttpStatusCode.NotFound);

            SetFieldsForUpdate(entityInDb, fromDto);

            await unitOfWork.RoomTypeRepository.UpdateAsync(entityInDb);
            await unitOfWork.SaveAsync();

            return mapper.Map<RoomTypeDTO>(entityInDb);
        }

        private async Task<RoomType> GetByIdAsync(int id)
            => await unitOfWork.RoomTypeRepository.GetByConditionAsync(rt => rt.Id == id);

    }
}
