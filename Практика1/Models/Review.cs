using System;
using System.Collections.Generic;

namespace Практика1.Models;

public partial class Review
{
    public long IdReview { get; set; }

    public long IdClient { get; set; }

    public long IdProduct { get; set; }

    public long Rating { get; set; }

    public string? Comment { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
