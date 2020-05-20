using Feedback.Repository.Entities;
using Feedback.Repository.Repository;
using Feedback.Services.FeedbackService;
using Feedback.Test.FakeData;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq;
using Xunit;

namespace Feedback.Test.Services
{

    public class FeedbackServicesTests : IClassFixture<FeedbackListFake>
    {

        private readonly ILogger<FeedbackService> _mockLogger = Substitute.For<ILogger<FeedbackService>>();
        private readonly IGenericRepository<FeedbackDto> _mockFeedbackRepository = Substitute.For<IGenericRepository<FeedbackDto>>();

        private readonly FeedbackListFake _feedbackListFake;
        public FeedbackServicesTests(FeedbackListFake feedbackListFake)
        {
            _feedbackListFake = feedbackListFake;
        }
        private FeedbackService GetFeedbackServiceService()
        {
            return new FeedbackService(_mockFeedbackRepository, _mockLogger);
        }


        #region GetFeedbackList
        [Fact]
        public  void GetAllFeedbacks_Should_Return_All_Feedbacks()
        {

            // Arrange
            var feedbackList = _feedbackListFake.GetFeedbackFromRepository().AsQueryable();

            _mockFeedbackRepository.GetAll().Returns(feedbackList);

            var ser = GetFeedbackServiceService();

            // Act 
            var result =  ser.GetAllFeedbacks();

            // Assert  
             result.Result.Count().Should().Be(2);
        }
        #endregion

        #region AddFeedback
        [Fact]
        public void AddFeedback_Return_True()
        {
            //arrange
            var storedFeedback = _feedbackListFake .GetFeedbackList().FirstOrDefault();

            _mockFeedbackRepository.Add(Arg.Any<FeedbackDto>()).Returns(true);
            var ser = GetFeedbackServiceService();

            //act 
            var result = ser.AddFeedback(storedFeedback);

            //assert  
            result.Result.Should().BeTrue();
        }

        [Fact]
        public void AddFeedback_Return_Exception()
        {
            //arrange
            var storedFeedback = _feedbackListFake.GetFeedbackFromRepository().FirstOrDefault();
            storedFeedback.HomeAddress = null;

            _mockFeedbackRepository.Add(storedFeedback).Returns(false);
            var ser = GetFeedbackServiceService();

            //act 
            var result = ser.AddFeedback(Arg.Any<Models.Feedback.Feedback>());

            //assert  
            var ex = result.Exception;
            ex.InnerExceptions.Count.Should().Be(1);
        }
        #endregion
    }
}
