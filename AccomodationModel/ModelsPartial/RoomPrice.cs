namespace AccomodationModel.Models;
public partial class RoomPrice
{
    public void SetPeriod(IEnumerable<RoomPrice> roomTypeRoomPrices)
    {
        DateTime currentDate = DateTime.Now;
        if (roomTypeRoomPrices == null || roomTypeRoomPrices.Count() == 0)
        {
            this.PeriodStrart = currentDate;
            this.PeriodEnd = null;
        }
        else
        {
            RoomPrice latest = roomTypeRoomPrices.OrderByDescending(rp => rp.PeriodStrart).First();
            latest.PeriodEnd = currentDate.AddSeconds(-1);
            this.PeriodStrart = currentDate;
        }
    }
}
