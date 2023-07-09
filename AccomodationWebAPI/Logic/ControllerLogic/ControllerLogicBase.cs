using AccomodationModel.AccomodationRepository;
using AccomodationModel.Models;
using AccomodationWebAPI.DTOs;
using AccomodationWebAPI.Logic.Factories;
using AutoMapper;

namespace AccomodationWebAPI.Logic.ControllerLogic
{
    public abstract class ControllerLogicBase<TGetDTO, TPostDTO, TPutDTO>
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        protected readonly IPagingFactroy pagingFactroy;

        public ControllerLogicBase(IUnitOfWork unitOfWork, IMapper mapper, IPagingFactroy pagingFactroy)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.pagingFactroy = pagingFactroy;
        }

        public abstract Task<PagingDTO<TGetDTO>> GetAllAsync(int pageNumber, int pageSize);
        public abstract Task<TGetDTO> GetAsync(int id);
        public abstract Task<TGetDTO> CreateAsync(TPostDTO entity);
        public abstract Task DeleteAsync(int id);
        public abstract Task<TGetDTO> UpdateAsync(TPutDTO entity);

        protected void SetFieldsForUpdate<T>(T sourceEntity, T updateEntity)
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.Name?.ToLower() == "id") continue;

                var propertyValue = prop.GetValue(updateEntity);
                if (propertyValue != null)
                    prop.SetValue(sourceEntity, propertyValue);
            }
        }
    }
}
