using System.ComponentModel.DataAnnotations;

namespace Feedback.Web.ViewModels
{
    public class HappyLevelLight
    {
        [Required(ErrorMessage = "Please tell us if you are happy with level of lighting")]

        public int? HappyWithLevelOfLighting { get; set; }
        public BackButton BackButton { get; set; }
    }
}
