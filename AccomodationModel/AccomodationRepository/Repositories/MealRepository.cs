using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class MealRepository : RepositoryBase<Meal>
    {
        public MealRepository(AccomodationContext context) : base(context)
        {
        }
    }
}
