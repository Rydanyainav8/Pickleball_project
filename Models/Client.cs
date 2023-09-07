using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pickleball_project.Models
{
    public class Client
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; } = string.Empty;
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstNameEmergencyContact { get; set; }
        public string LastNameEmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }
        public string PlayedCategory { get; set; }
        public string Group { get; set; }
    }
}
