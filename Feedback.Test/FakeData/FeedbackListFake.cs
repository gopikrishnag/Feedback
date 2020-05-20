using System.Collections.Generic;
using Feedback.Repository.Entities;

namespace Feedback.Test.FakeData
{
    public class FeedbackListFake
    {
        public IEnumerable<Models.Feedback.Feedback> GetFeedbackList()
        {
            return new List<Models.Feedback.Feedback>()
                   {
                       new Models.Feedback.Feedback
                       {
                           Name = "Gopi",
                           Email = "Gopi@yahoo.com",
                           HomeAddress = "Bexley",
                           HomePostcode = "Da7 6qj",
                           FeedbackId = 1,
                           LevelOfBrightness = 7,
                           HappyWithLevelOfLighting = true
                       },
                       new Models.Feedback.Feedback
                       {
                           Name = "Govind",
                           Email = "Govid@yahoo.com",
                           HomeAddress = "Kent",
                           HomePostcode = "Da7 6qj",
                           FeedbackId = 2,
                           LevelOfBrightness = 8,
                           HappyWithLevelOfLighting = true
                       }
                   };
        }
        public IEnumerable<FeedbackDto> GetFeedbackFromRepository()
        {
            return new List<FeedbackDto>
                   {
                       new FeedbackDto
                       {
                            FeedbackId = 1,
                            Email = "Gopi@yahoo.com",
                            HomeAddress = "Bexley",
                            HomePostcode = "DA7 6QJ",
                            Name = "Gopi",
                            LevelOfBrightness = 7,
                            HappyWithLevelOfLighting = true
                       },
                       new FeedbackDto
                       {
                           FeedbackId = 2,
                           Email = "Govind@yahoo.com",
                           HomeAddress = "Kent",
                           HomePostcode = "DA7 6QJ",
                           Name = "Govind",
                           LevelOfBrightness = 8,
                           HappyWithLevelOfLighting = true
                       }
                   };
        }
    }
}
