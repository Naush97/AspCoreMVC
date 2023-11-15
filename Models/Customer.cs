using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task1.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }
        [Required]
        [Range(18,100,ErrorMessage ="Enter age between 18-100 only")]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Number should be only 10 digits")]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage ="Enter valid Email")]
        public string Email { get; set; }
        [Required]
        [ForeignKey("Country")]
        [DisplayName("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        [Required]
        [ForeignKey("City")]
        [DisplayName("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
