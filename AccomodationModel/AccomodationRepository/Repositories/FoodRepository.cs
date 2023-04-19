using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class FoodRepository : RepositoryBase<Food>
    {
        public FoodRepository(AccomodationContext context) : base(context)
        {
        }
    }
}
