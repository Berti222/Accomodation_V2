using System;
using System.Collections.Generic;

namespace AccomodationModel.Models;

public partial class RoomType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Size { get; set; }

    public int? NumberOfBed { get; set; }

    public string? Direction { get; set; }

    public string? Discription { get; set; }

    public string? Type { get; set; }

    public string? ImageUrl { get; set; }

    public byte? Capacity { get; set; }

    public short? NumberOfRoom { get; set; }

    public virtual ICollection<RoomPrice> RoomPrices { get; set; } = new List<RoomPrice>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
