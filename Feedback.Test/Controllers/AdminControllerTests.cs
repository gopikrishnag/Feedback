using Feedback.Services.FeedbackService;
using Feedback.Test.FakeData;
using Feedback.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Feedback.Test.Controllers
{
    public class AdminControllerTests : IClassFixture<FeedbackListFake>
    {
        private readonly ILogger<AdminController> _mockLogger = Substitute.For<ILogger<AdminController>>();
        private readonly IFeedbackService _mockFeedbackService = Substitute.For<IFeedbackService>();

        private readonly FeedbackListFake _feedbackListFake;
        private AdminController GetAdminControllerObject()
        {
            return new AdminController(_mockLogger,
                                          _mockFeedbackService);

        }

        public AdminControllerTests(FeedbackListFake feedbackListFake)
        {
            _feedbackListFake = feedbackListFake;
        }



        [Fact]
        public async Task Admin_Get_Should_Return_Index_View()
        {
            // Arrange
            var storedFeedback = _feedbackListFake.GetFeedbackList().AsQueryable();

            _mockFeedbackService.GetAllFeedbacks().ReturnsForAnyArgs(storedFeedback);
            var sut = GetAdminControllerObject();
            //act
            var result = await sut.Index();

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("Index");

        }


    }
}
