namespace AccomodationWebAPI.DTOs.GetDTOs
{
    public class GuestDTO : IDTOWithId
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumer { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<AllergenicDTO> Allergenics { get; set; }
    }
}
