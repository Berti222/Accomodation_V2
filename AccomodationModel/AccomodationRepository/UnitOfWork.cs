using AccomodationModel.AccomodationRepository.Repositories;
using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccomodationContext context;

        private AllergenicRepository _allergenicRepository;
        private EquipmentRepository _equipmentRepository;
        private FoodRepository _foodRepository;
        private GuestRepository _guestRepository;
        private MealRepository _mealRepository;
        private ReservationRepository _reservationRepository;
        private RoomPriceRepository _roomPriceRepository;
        private RoomRepository _roomRepository;
        private RoomTypeRepository _roomTypeRepository;
        private ServiceRepository _serviceRepository;

        public UnitOfWork(AccomodationContext context)
        {
            this.context = context;
        }

        public AllergenicRepository AllergenicRepository => _allergenicRepository ?? new AllergenicRepository(context);

        public EquipmentRepository EquipmentRepository => _equipmentRepository ?? new EquipmentRepository(context);

        public FoodRepository FoodRepository => _foodRepository ?? new FoodRepository(context);

        public GuestRepository GuestRepository => _guestRepository ?? new GuestRepository(context);

        public MealRepository MealRepository => _mealRepository ?? new MealRepository(context);

        public ReservationRepository ReservationRepository => _reservationRepository ?? new ReservationRepository(context);

        public RoomPriceRepository RoomPriceRepository => _roomPriceRepository ?? new RoomPriceRepository(context);

        public RoomRepository RoomRepository => _roomRepository ?? new RoomRepository(context);

        public RoomTypeRepository RoomTypeRepository => _roomTypeRepository ?? new RoomTypeRepository(context);

        public ServiceRepository ServiceRepository => _serviceRepository ?? new ServiceRepository(context);

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
