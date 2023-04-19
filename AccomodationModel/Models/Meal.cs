using System;
using System.Collections.Generic;

namespace AccomodationModel.Models;

public partial class Meal
{
    public int Id { get; set; }

    public string MealType { get; set; }

    public decimal? Price { get; set; }

    public DateTime? ServiceDate { get; set; }

    public int? GuestId { get; set; }

    public int? FoodId { get; set; }

    public int? ReservationId { get; set; }

    public virtual Food Food { get; set; }

    public virtual Guest Guest { get; set; }

    public virtual Reservation Reservation { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
}
