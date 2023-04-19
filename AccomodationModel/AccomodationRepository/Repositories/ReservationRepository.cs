using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservation>
    {
        public ReservationRepository(AccomodationContext context) : base(context)
        {
        }
    }
}
