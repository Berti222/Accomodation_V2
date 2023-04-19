using System;
using System.Collections.Generic;

namespace AccomodationModel.Models;

public partial class Guest
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string IdentityNumber { get; set; }

    public string Email { get; set; }

    public string PhoneNumer { get; set; }

    public DateTime? BirthDate { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual ICollection<Allergenic> Allergenics { get; set; } = new List<Allergenic>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
