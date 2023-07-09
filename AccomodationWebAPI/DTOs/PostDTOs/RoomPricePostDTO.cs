namespace AccomodationWebAPI.DTOs.PostDTOs
{
    public class RoomPricePostDTO
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? ExtraBedPrice { get; set; }
        public int? RoomTypeId { get; set; }
    }
}
