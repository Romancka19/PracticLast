using System;
using System.Collections.Generic;

namespace Практика1.Models;

public partial class Service
{
    public long IdService { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> IdOrders { get; set; } = new List<Order>();
}
