using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class EquipmentRepository : RepositoryBase<Equipment>
    {
        public EquipmentRepository(AccomodationContext context) : base(context)
        {
        }
    }
}
