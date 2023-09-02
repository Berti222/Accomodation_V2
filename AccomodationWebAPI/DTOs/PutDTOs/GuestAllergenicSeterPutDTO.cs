namespace AccomodationWebAPI.DTOs.PutDTOs
{
    public class GuestAllergenicSeterPutDTO
    {
        public int GuestId { get; set; }
        public List<int> AllergenicIds { get; set; }
    }
}
