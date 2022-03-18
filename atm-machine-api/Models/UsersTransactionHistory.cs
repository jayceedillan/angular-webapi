using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace atm_machine_api.Models
{
    public class UsersTransactionHistory
    {
        [Key]
        [Display(Name = "Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int pinNo { get; set; }

        [Required]
        public int amount { get; set; }

        [Required]
        public DateTime transactionDate { get; set; } = DateTime.Now;

        [Required]
        public string typeOfTransaction { get; set; }

    }
}