using System;
using System.Collections.Generic;

namespace Практика1.Models;

public partial class Order
{
    public long IdOrder { get; set; }

    public long IdClient { get; set; }

    public long IdEmployee { get; set; }

    public DateOnly? Date { get; set; }

    public string? Status { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    public virtual ICollection<Product> IdProducts { get; set; } = new List<Product>();

    public virtual ICollection<Service> IdServices { get; set; } = new List<Service>();
}
