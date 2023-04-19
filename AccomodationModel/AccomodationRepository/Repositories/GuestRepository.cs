using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class GuestRepository : RepositoryBase<Guest>
    {
        public GuestRepository(AccomodationContext context) : base(context)
        {
        }
    }
}
