using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class RoomPriceRepository : RepositoryBase<RoomPrice>
    {
        public RoomPriceRepository(AccomodationContext context) : base(context)
        {
        }
    }
}
