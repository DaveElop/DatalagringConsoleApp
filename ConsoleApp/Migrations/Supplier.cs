﻿using System;
using System.Collections.Generic;

namespace ConsoleApp.Migrations;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactEmail { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
