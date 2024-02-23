using System.ComponentModel.DataAnnotations;

namespace HamburgerMVC4.Models
{
    public class MailViewModel
    {
        public string Sender { get; set; } = null!;
        public string Reciever { get; set; } = null!;

        [Required]
        public string Subject { get; set; } = null!;

        [Required]
        public string Body { get; set; } = null!;
    }
}
