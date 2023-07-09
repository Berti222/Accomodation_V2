using AccomodationModel.Models;

namespace AccomodationModel.AccomodationRepository.Repositories
{
    public class RoomPriceRepository : RepositoryBase<RoomPrice>
    {
        public RoomPriceRepository(AccomodationContext context) : base(context)
        {
        }

        public override async Task CreateAsync(RoomPrice entity)
        {
            var roomTpyeRelatedPrices = context.RoomPrices.Where(rp => rp.RoomTypeId == entity.RoomTypeId);
            entity.SetPeriod(roomTpyeRelatedPrices);
            await base.CreateAsync(entity);
        }
    }
}
