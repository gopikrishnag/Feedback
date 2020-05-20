using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Feedback.Models.Constants;

namespace Feedback.Web.ViewModels
{
    public class UserEmail
    {
        [Required(ErrorMessage = "Please enter your email")]
        [MinLength(2, ErrorMessage = "Please enter more than 2 characters")]
        [MaxLength(50)]
        [RegularExpression(TextPattern.Email, ErrorMessage = "Please enter valid email address")]
        [DataType(DataType.Text)]
        [Description("Email")]
        public string Email { get; set; }
        public BackButton BackButton { get; set; }
    }
}
