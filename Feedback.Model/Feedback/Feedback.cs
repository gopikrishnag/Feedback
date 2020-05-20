using System;
using System.ComponentModel;

namespace Feedback.Models.Feedback
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        [Description("Full Name")]
        public string Name { get; set; }
        [Description("Email")]
        public string Email { get; set; }
        [Description("Postcode")]
        public string HomePostcode { get; set; }
        [Description("Address")]
        public string HomeAddress { get; set; }
        [Description("Happy Level")]
        public bool HappyWithLevelOfLighting { get; set; }
        [Description("Brightness Level")]
        public int LevelOfBrightness { get; set; }
        [Description("Submitted On")]
        public DateTime CreatedOn { get; set; }
    }
}
