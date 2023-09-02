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
    public class GuestLogic : ControllerLogicBase<GuestDTO, GuestPostDTO, GuestPutDTO>
    {
        public GuestLogic(IUnitOfWork unitOfWork, IMapper mapper, IPagingFactroy pagingFactroy)
            : base(unitOfWork, mapper, pagingFactroy)
        {
        }

        public override async Task<GuestDTO> CreateAsync(GuestPostDTO entity)
        {
            var enityWithTheSameName = await unitOfWork.GuestRepository.GetByConditionAsync(g => g.Email == entity.Email);

            if (enityWithTheSameName is not null) throw new HTTPStatusException("Entiy with the same name already exists.", HttpStatusCode.BadRequest);

            try
            {
                Guest entityForCreate = mapper.Map<GuestPostDTO, Guest>(entity);
                await unitOfWork.GuestRepository.CreateAsync(entityForCreate);
                await unitOfWork.SaveAsync();

                return mapper.Map<Guest, GuestDTO>(entityForCreate);
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

            unitOfWork.GuestRepository.Delete(entityInDb);
            await unitOfWork.SaveAsync();
        }

        public override async Task<PagingDTO<GuestDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entitiesInDb = await unitOfWork.GuestRepository.GetAllAsync(tracked: false, includeProperties: "Allergenics");
            if (entitiesInDb == null || entitiesInDb.Count() == 0) throw new HTTPStatusException("No etities found.", HttpStatusCode.NotFound);

            var mappedEntities = mapper.Map<List<GuestDTO>>(entitiesInDb);

            var page = pagingFactroy.Create(mappedEntities, pageNumber, pageSize);
            return page;
        }

        public override async Task<GuestDTO> GetAsync(int id)
        {
            var entityFoundInDB = await unitOfWork.GuestRepository.GetByConditionAsync(g => g.Id == id, includeProperties: "Allergenics");

            if (entityFoundInDB is null) throw new HTTPStatusException("Not found.", HttpStatusCode.NotFound);

            return mapper.Map<GuestDTO>(entityFoundInDB);
        }

        public override async Task<GuestDTO> UpdateAsync(GuestPutDTO entity)
        {
            if (entity is null) throw new HTTPStatusException("Entity is null", HttpStatusCode.BadRequest);

            var fromDto = mapper.Map<Guest>(entity);
            var entityInDb = await GetByIdAsync(entity.Id);
            if (entityInDb is null) throw new HTTPStatusException("Entity with the id not exist in DB.", HttpStatusCode.NotFound);

            SetFieldsForUpdate(entityInDb, fromDto);

            await unitOfWork.GuestRepository.UpdateAsync(entityInDb);
            await unitOfWork.SaveAsync();

            return mapper.Map<GuestDTO>(entityInDb);
        }

        public async Task<GuestDTO> AddAllergenicsAsync(GuestAllergenicSeterPutDTO entity)
        {
            if (entity is null && entity.GuestId == 0) throw new HTTPStatusException("Entity is null, or Guest id is default", HttpStatusCode.BadRequest);

            var guestInDb = await GetByIdAsync(entity.GuestId, nameof(Guest.Allergenics));

            var idsForRelationCreate = entity.AllergenicIds.Where(id => !guestInDb.Allergenics.Select(a => a.Id).Contains(id));
            var allergenicsToAdd = await unitOfWork.AllergenicRepository.GetAllAsync(a => idsForRelationCreate.Contains(a.Id));

            foreach (var allergenicToAdd in allergenicsToAdd)
                guestInDb.Allergenics.Add(allergenicToAdd);

            await unitOfWork.SaveAsync();

            return mapper.Map<GuestDTO>(guestInDb);
        }

        public async Task<GuestDTO> RemoveAllergenicsAsync(GuestAllergenicSeterPutDTO entity)
        {
            if (entity is null && entity.GuestId == 0) throw new HTTPStatusException("Entity is null, or Guest id is default", HttpStatusCode.BadRequest);

            var guestInDb = await GetByIdAsync(entity.GuestId, nameof(Guest.Allergenics));

            var newAllergenicsCollection = guestInDb.Allergenics.Where(a => !entity.AllergenicIds.Contains(a.Id));
            guestInDb.Allergenics = newAllergenicsCollection.ToList();

            await unitOfWork.SaveAsync();

            return mapper.Map<GuestDTO>(guestInDb);
        }

        private async Task<Guest> GetByIdAsync(int id, params string[] includeNames)
        {
            if (includeNames is null)
                return await unitOfWork.GuestRepository.GetByConditionAsync(g => g.Id == id);
            else
                return await unitOfWork.GuestRepository.GetByConditionAsync(g => g.Id == id, includeNames);
        }

    }
}
