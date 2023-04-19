using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class RoomRepository : RepositoryBase<Room>
    {
        public RoomRepository(AccomodationContext context) : base(context)
        {
        }
    }
}
