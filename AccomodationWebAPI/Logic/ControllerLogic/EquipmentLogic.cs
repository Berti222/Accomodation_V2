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
    public class EquipmentLogic : ControllerLogicBase<EquipmentDTO, EquipmentPostDTO, EquipmentPutDTO>
    {
        public EquipmentLogic(IUnitOfWork unitOfWork, IMapper mapper, IPagingFactroy pagingFactroy) : base(unitOfWork, mapper, pagingFactroy)
        {
        }

        public override async Task<EquipmentDTO> CreateAsync(EquipmentPostDTO entity)
        {
            var enityWithTheSameName = await unitOfWork.EquipmentRepository.GetByConditionAsync(a => a.Name == entity.Name);

            if (enityWithTheSameName is not null) throw new HTTPStatusException("Entiy with the same name already exists.", HttpStatusCode.BadRequest);

            try
            {
                Equipment entityForCreate = mapper.Map<EquipmentPostDTO, Equipment>(entity);
                await unitOfWork.EquipmentRepository.CreateAsync(entityForCreate);
                await unitOfWork.SaveAsync();

                return mapper.Map<Equipment, EquipmentDTO>(entityForCreate);
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

            unitOfWork.EquipmentRepository.Delete(entityInDb);
            await unitOfWork.SaveAsync();
        }

        public override async Task<PagingDTO<EquipmentDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entitiesInDb = await unitOfWork.EquipmentRepository.GetAllAsync(tracked: false);
            if (entitiesInDb == null || entitiesInDb.Count() == 0) throw new HTTPStatusException("No etities found.", HttpStatusCode.NotFound);

            var mappedEntities = mapper.Map<List<EquipmentDTO>>(entitiesInDb);

            var page = pagingFactroy.Create(mappedEntities, pageNumber, pageSize);
            return page;
        }

        public override async Task<EquipmentDTO> GetAsync(int id)
        {
            var entityFoundInDB = await GetByIdAsync(id);

            if (entityFoundInDB is null) throw new HTTPStatusException("Not found.", HttpStatusCode.NotFound);

            return mapper.Map<EquipmentDTO>(entityFoundInDB);
        }

        public override async Task<EquipmentDTO> UpdateAsync(EquipmentPutDTO entity)
        {
            if (entity is null) throw new HTTPStatusException("Entity is null", HttpStatusCode.BadRequest);

            var fromDto = mapper.Map<Equipment>(entity);
            var entityInDb = await GetByIdAsync(entity.Id);
            if (entityInDb is null) throw new HTTPStatusException("Entity with the id not exist in DB.", HttpStatusCode.NotFound);

            SetFieldsForUpdate(entityInDb, fromDto);

            await unitOfWork.EquipmentRepository.UpdateAsync(entityInDb);
            await unitOfWork.SaveAsync();

            return mapper.Map<EquipmentDTO>(entityInDb);
        }

        private async Task<Equipment> GetByIdAsync(int id)
            => await unitOfWork.EquipmentRepository.GetByConditionAsync(a => a.Id == id);
    }
}
