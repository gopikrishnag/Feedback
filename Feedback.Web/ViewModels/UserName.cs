using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Feedback.Web.ViewModels
{
    public class UserName  
    {
        [Required(ErrorMessage = "Please enter your full name")]
        [MinLength(2, ErrorMessage = "Please enter more than 2 characters")]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        [Description("Full name")]
        public string FullName { get; set; }
        public BackButton BackButton { get; set; }
    }
}
