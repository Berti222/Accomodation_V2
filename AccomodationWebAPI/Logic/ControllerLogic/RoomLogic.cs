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
using System.Runtime.CompilerServices;

namespace AccomodationWebAPI.Logic.ControllerLogic
{
    public class RoomLogic : ControllerLogicBase<RoomDTO, RoomPostDTO, RoomPutDTO>
    {
        public RoomLogic(IUnitOfWork unitOfWork, IMapper mapper, IPagingFactroy pagingFactroy)
            : base(unitOfWork, mapper, pagingFactroy)
        {
        }

        public async Task<EquipmentDTO> GetRomsForEquipmnet()
        {
            var res = await unitOfWork.EquipmentRepository.GetByConditionAsync(x => x.Id == 1, includeProperties: "Rooms");
            //var resList = res.ToList();
            return mapper.Map<EquipmentDTO>(res);
        }

        public override async Task<RoomDTO> CreateAsync(RoomPostDTO entity)
        {
            var enityWithTheSameName = await unitOfWork.RoomRepository.GetByConditionAsync(a => a.RoomNumber == entity.RoomNumber);

            if (enityWithTheSameName is not null) throw new HTTPStatusException("Entiy with the same room number already exists.", HttpStatusCode.BadRequest);

            try
            {
                Room entityForCreate = mapper.Map<RoomPostDTO, Room>(entity);
                await unitOfWork.RoomRepository.CreateAsync(entityForCreate);
                await unitOfWork.SaveAsync();

                return mapper.Map<Room, RoomDTO>(entityForCreate);
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

            unitOfWork.RoomRepository.Delete(entityInDb);
            await unitOfWork.SaveAsync();
        }

        public override async Task<PagingDTO<RoomDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entitiesInDb = await unitOfWork.RoomRepository.GetAllAsync(null, false, "RoomType", "Equipments");
            if (entitiesInDb == null || entitiesInDb.Count() == 0) throw new HTTPStatusException("No etities found.", HttpStatusCode.NotFound);

            var mappedEntities = mapper.Map<List<RoomDTO>>(entitiesInDb);

            var page = pagingFactroy.Create(mappedEntities, pageNumber, pageSize);
            return page;
        }

        public override async Task<RoomDTO> GetAsync(int id)
        {
            //var entityFoundInDB = await GetByIdAsync(id, new string[] { "RoomType" }); //, "Equipments"});
            var entityFoundInDB = await GetByIdAsync(id, new string[] { "RoomType", "Equipments"});

            if (entityFoundInDB is null) throw new HTTPStatusException("Not found.", HttpStatusCode.NotFound);

            return mapper.Map<RoomDTO>(entityFoundInDB);
        }

        public override async Task<RoomDTO> UpdateAsync(RoomPutDTO entity)
        {
            if (entity is null) throw new HTTPStatusException("Entity is null", HttpStatusCode.BadRequest);

            var fromDto = mapper.Map<Room>(entity);
            var entityInDb = await GetByIdAsync(entity.Id);
            if (entityInDb is null) throw new HTTPStatusException("Entity with the id not exist in DB.", HttpStatusCode.NotFound);

            SetFieldsForUpdate(entityInDb, fromDto);

            await unitOfWork.RoomRepository.UpdateAsync(entityInDb);
            await unitOfWork.SaveAsync();

            return mapper.Map<RoomDTO>(entityInDb);
        }

        private async Task<Room> GetByIdAsync(int id, string[] includeProperties = null)
            => await unitOfWork.RoomRepository.GetByConditionAsync(a => a.Id == id, includeProperties: includeProperties);
    }
}
