using System;
using System.Collections.Generic;

namespace Практика1.Models;

public partial class Product
{
    public long IdProduct { get; set; }

    public long IdProvider { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public decimal? Price { get; set; }

    public long? Count { get; set; }

    public virtual Provider IdProviderNavigation { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Order> IdOrders { get; set; } = new List<Order>();
}
