using System;
using System.Collections.Generic;

namespace AccomodationModel.Models;

public partial class Room
{
    public int Id { get; set; }

    public short? RoomNumber { get; set; }

    public int? RoomTypeId { get; set; }

    public virtual RoomType RoomType { get; set; }

    public virtual ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
