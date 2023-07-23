namespace AccomodationWebAPI.DTOs.GetDTOs
{
    public partial class EquipmentDTO : IDTOWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
