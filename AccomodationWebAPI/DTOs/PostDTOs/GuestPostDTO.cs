namespace AccomodationWebAPI.DTOs.PostDTOs
{
    public class GuestPostDTO
    {
        public string FullName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumer { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
