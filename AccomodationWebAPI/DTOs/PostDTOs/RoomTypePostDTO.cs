namespace AccomodationWebAPI.DTOs.PostDTOs
{
    public class RoomTypePostDTO
    {
        public string Name { get; set; }
        public int? Size { get; set; }
        public int? NumberOfBed { get; set; }
        public string Direction { get; set; }
        public string Discription { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public byte? Capacity { get; set; }
    }
}
