using AccomodationModel.AccomodationRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccomodationModel.AccomodationRepository
{
    public interface IUnitOfWork
    {
        public AllergenicRepository AllergenicRepository { get; }
        public EquipmentRepository EquipmentRepository { get; }
        public FoodRepository FoodRepository { get; }
        public GuestRepository GuestRepository { get; }
        public MealRepository MealRepository { get; }
        public ReservationRepository ReservationRepository { get; }
        public RoomPriceRepository RoomPriceRepository { get; }
        public RoomRepository RoomRepository { get; }
        public RoomTypeRepository RoomTypeRepository { get; }
        public ServiceRepository ServiceRepository { get; }
        Task SaveAsync();
    }
}
