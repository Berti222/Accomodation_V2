using System;
using System.Collections.Generic;

namespace AccomodationModel.Models;

public partial class Equipment
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
