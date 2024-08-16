using System.ComponentModel.DataAnnotations;

namespace UserService.Model
{
    /// <summary>
    /// User details model
    /// </summary>
    public class UserDetails
    {
        public int Id { get; set; }

        [Required]
        public string firstname { get; set; }

        [Required]
        public string lastname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid Email Address")]

        public string email { get; set; }

        [Required]
        public string address { get; set; }
    }
}
