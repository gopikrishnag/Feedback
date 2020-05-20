using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Feedback.Web.ViewModels
{
    public class BrightnessScore
    {
        [Required(ErrorMessage = "Please select the level of brightness")]
        public string BringhtnessScoreValue { get; set; }
        public List<SelectListItem> ListBrightnessScores { get; set; }
        public BackButton BackButton { get; set; }
         
    }
}
