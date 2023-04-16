using System;
using System.Collections.Generic;

namespace AccomodationModel.Models;

public partial class Food
{
    public int Id { get; set; }

    public string? FoodType { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();

    public virtual ICollection<Allergenic> Allergenics { get; set; } = new List<Allergenic>();

    public virtual ICollection<Meal> MealsNavigation { get; set; } = new List<Meal>();
}
