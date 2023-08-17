namespace AccomodationWebAPI.DTOs.PostDTOs
{
    public class RoomPostDTO
    {
        public short? RoomNumber { get; set; }
        public int? RoomTypeId { get; set; }
        public IEnumerable<int> Equipments { get; set; }
    }
}
