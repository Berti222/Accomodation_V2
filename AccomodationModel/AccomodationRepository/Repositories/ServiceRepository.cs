using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class ServiceRepository : RepositoryBase<Service>
    {
        public ServiceRepository(AccomodationContext context) : base(context)
        {
        }
    }
}
