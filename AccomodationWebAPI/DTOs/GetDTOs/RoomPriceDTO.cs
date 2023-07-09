using AccomodationModel.Models;

namespace AccomodationWebAPI.DTOs.GetDTOs
{
    public class RoomPriceDTO : IDTOWithId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public DateTime? CreatedOn { get; set; }

        public decimal? ExtraBedPrice { get; set; }

        public DateTime? PeriodStrart { get; set; }

        public DateTime? PeriodEnd { get; set; }
        public int? RoomTypeId { get; set; }

    }
}
