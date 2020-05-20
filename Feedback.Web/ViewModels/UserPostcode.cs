using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Feedback.Models.Constants;

namespace Feedback.Web.ViewModels
{
    public class UserPostcode
    {
        [Required(ErrorMessage = "Please enter your postcode")]
        [MinLength(2, ErrorMessage = "Please enter more than 2 characters")]
        [MaxLength(10)]
        [RegularExpression(TextPattern.Postcode, ErrorMessage = "Please enter valid postcode")]
        [DataType(DataType.PostalCode)]
        [Description("Postcode")]
        public string Postcode { get; set; }
        public BackButton BackButton { get; set; }
    }
}
