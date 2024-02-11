using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Entities
{
    public class AddressEntity
    {
        [Key]
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}