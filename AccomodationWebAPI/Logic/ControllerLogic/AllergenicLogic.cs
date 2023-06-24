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
    public class AllergenicLogic : ControllerLogicBase<AllergenicDTO, AllergenicPostDTO, AllergenicPutDTO>
    {
        public AllergenicLogic(IUnitOfWork unitOfWork, IMapper mapper, IPagingFactroy pagingFactroy) 
            : base(unitOfWork, mapper, pagingFactroy)
        {
        }

        public override async Task CreateAsync(AllergenicPostDTO entity)
        {
            var enityWithTheSameName = await unitOfWork.AllergenicRepository.GetAllAsync(a => a.Name == entity.Name);

            if (enityWithTheSameName is not null) throw new HTTPStatusException("Entiy with the same name already exists.", HttpStatusCode.BadRequest);

            try
            {
                Allergenic entityForCreate = mapper.Map<AllergenicPostDTO, Allergenic>(entity);
                await unitOfWork.AllergenicRepository.CreateAsync(entityForCreate);
            }
            catch (Exception ex)
            {
                throw new HTTPStatusException(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public override async Task DeleteAsync(int id)
        {
            var entityInDb = await GetByIdAsync(id);

            if(entityInDb is null) throw new HTTPStatusException("Entiy not exists in DB.", HttpStatusCode.NotFound);

            unitOfWork.AllergenicRepository.Delete(entityInDb);
            await unitOfWork.SaveAsync();
        }

        public override async Task<PagingDTO<AllergenicDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entitiesInDb = await unitOfWork.AllergenicRepository.GetAllAsync(tracked: false);
            if(entitiesInDb == null || entitiesInDb.Count() == 0) throw new HTTPStatusException("No etities found.", HttpStatusCode.NotFound);

            var mappedEntities = mapper.Map<List<AllergenicDTO>>(entitiesInDb);

            var page = pagingFactroy.Create(mappedEntities, pageNumber, pageSize);
            return page;
        }

        public override async Task<AllergenicDTO> GetAsync(int id)
        {
            var entityFoundInDB = await GetByIdAsync(id);

            if(entityFoundInDB is null) throw new HTTPStatusException("Not found.", HttpStatusCode.NotFound);

            return mapper.Map<AllergenicDTO>(entityFoundInDB);
        }

        public override async Task<AllergenicDTO> UpdateAsync(AllergenicPutDTO entity)
        {
            if(entity is null) throw new HTTPStatusException("Entity is null", HttpStatusCode.BadRequest);

            var fromDto = mapper.Map<Allergenic>(entity);
            var entityInDb = await GetByIdAsync(entity.Id);
            if(entityInDb is null) throw new HTTPStatusException("Entity with the id not exist in DB.", HttpStatusCode.NotFound);

            SetFieldsForUpdate(entityInDb, fromDto);

            await unitOfWork.AllergenicRepository.UpdateAsync(entityInDb);
            await unitOfWork.SaveAsync();

            return mapper.Map<AllergenicDTO>(entityInDb);
        }

        private async Task<Allergenic> GetByIdAsync(int id)
            => await unitOfWork.AllergenicRepository.GetByConditionAsync(a => a.Id == id);
    }
}
