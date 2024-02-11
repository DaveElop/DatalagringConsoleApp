using System;
using System.Collections.Generic;

namespace ConsoleApp.Migrations;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactEmail { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
