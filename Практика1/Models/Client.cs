using System;
using System.Collections.Generic;

namespace Практика1.Models;

public partial class Client
{
    public long IdClient { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Mail { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
