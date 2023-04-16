using System;
using System.Collections.Generic;

namespace AccomodationModel.Models;

public partial class Service
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? TypeOfPayment { get; set; }

    public string? Discription { get; set; }
}
