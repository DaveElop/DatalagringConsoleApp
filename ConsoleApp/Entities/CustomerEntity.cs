using ConsoleApp.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CustomerEntity
{
    [Key]
    public int? CustomerID { get; set; }
    public int AddressID { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;
    public AddressEntity? Address { get; set; }

    public List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}
