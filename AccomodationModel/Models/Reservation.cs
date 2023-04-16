using System;
using System.Collections.Generic;

namespace AccomodationModel.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public DateTime? DateOfArrive { get; set; }

    public DateTime? DateOfDeparture { get; set; }

    public DateTime? CreatedAt { get; set; }

    public decimal? Price { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? Discount { get; set; }

    public string? Type { get; set; }

    public short? NumberOfAdoult { get; set; }

    public short? NumberOfChild { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
