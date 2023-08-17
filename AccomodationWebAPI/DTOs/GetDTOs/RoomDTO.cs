namespace AccomodationWebAPI.DTOs.GetDTOs
{
    public class RoomDTO : IDTOWithId
    {
        public int Id { get; set; }
        public short? RoomNumber { get; set; }
        public RoomTypeDTO RoomType { get; set; }
        public List<EquipmentDTO> Equipments { get; set; }
    }
}
