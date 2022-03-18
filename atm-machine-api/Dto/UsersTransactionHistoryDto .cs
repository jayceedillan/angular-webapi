using System.ComponentModel.DataAnnotations;

namespace atm_machine_api.Dto
{
    public class UsersTransactionHistoryDto
    {
        [Required]
        public int pinNo { get; set; }

        [Required]
        public int amount { get; set; }

        [Required]
        public DateTime transactionDate { get; set; }

        [Required]
        public string typeOfTransaction { get; set; }
    }
}