using System.ComponentModel.DataAnnotations;
namespace Task1.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }

    }
}
