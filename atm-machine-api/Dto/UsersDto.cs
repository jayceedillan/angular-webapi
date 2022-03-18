using System.ComponentModel.DataAnnotations;

namespace atm_machine_api.Dto
{
    public class UsersDto
    {
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