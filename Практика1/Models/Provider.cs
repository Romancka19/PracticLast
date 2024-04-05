using System;
using System.Collections.Generic;

namespace Практика1.Models;

public partial class Provider
{
    public long IdProvider { get; set; }

    public string Name { get; set; } = null!;

    public string ContactPerson { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
