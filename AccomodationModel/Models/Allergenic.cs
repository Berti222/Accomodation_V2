using System;
using System.Collections.Generic;

namespace AccomodationModel.Models;

public partial class Allergenic
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
}
