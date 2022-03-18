using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atm_machine_api.Models
{
    public class Users
    {
        [Key]
        [Display(Name = "User Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string firstName { get; set; } = string.Empty;

        [Required]
        public string lastName { get; set; } = string.Empty;

        [Required]
        public int cardNo { get; set; } = 0;

        [Required]
        public int balance { get; set; }

        [Required]
        public int pinNo { get; set; }

    }
}