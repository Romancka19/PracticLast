using System;
using System.Collections.Generic;

namespace Практика1.Models;

public partial class Account
{
    public long IdEmployee { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Role { get; set; }

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;
}
