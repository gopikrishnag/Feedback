namespace Feedback.Web.ViewModels
{
    public class CheckYourAnswers
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string HomePostcode { get; set; }
        public string HomeAddress { get; set; }
        public string HappyLevelOfLighting { get; set; }
        public string LevelOfBrightness { get; set; }
        public BackButton BackButton { get; set; }

        public string Confirm { get; set; }
    }
}
