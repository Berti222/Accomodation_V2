using System;
using System.Collections.Generic;

namespace AccomodationModel.Models;

public partial class RoomPrice
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedOn { get; set; }

    public decimal? ExtraBedPrice { get; set; }

    public DateTime? PeriodStrart { get; set; }

    public DateTime? PeriodEnd { get; set; }

    public int? RoomTypeId { get; set; }

    public virtual RoomType? RoomType { get; set; }
}
