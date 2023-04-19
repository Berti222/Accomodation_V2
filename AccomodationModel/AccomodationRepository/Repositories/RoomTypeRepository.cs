using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class RoomTypeRepository : RepositoryBase<RoomType>
    {
        public RoomTypeRepository(AccomodationContext context) : base(context)
        {
        }
    }
}
