using Feedback.Helpers.CacheHelper;
using Feedback.Models.Settings;
using Feedback.Services.AddressService;
using Feedback.Services.FeedbackService;
using Feedback.Test.FakeData;
using Feedback.Web.Controllers;
using Feedback.Web.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace Feedback.Test.Controllers
{
    public class FeedbackControllerTests : IClassFixture<FeedbackListFake>
    {
        private readonly ICacheHelpers _mockCacheHelper = Substitute.For<ICacheHelpers>();
        private readonly ILogger<FeedbackController> _mockLogger = Substitute.For<ILogger<FeedbackController>>();
        private readonly IAddressService _mockAddressService = Substitute.For<IAddressService>();
        private readonly IFeedbackService _mockFeedbackService = Substitute.For<IFeedbackService>();

        private readonly WebSettings _mockWebSettings = Substitute.For<WebSettings>();

        private readonly FeedbackListFake _feedbackListFake;


        private FeedbackController GetFeedbackControllerObject()
        {
            return new FeedbackController(_mockLogger,
                                          _mockCacheHelper,
                                          _mockAddressService,
                                          _mockFeedbackService,
                                          _mockWebSettings);

        }


        public FeedbackControllerTests(FeedbackListFake feedbackListFake)
        {
            _feedbackListFake = feedbackListFake;
        }


        #region Username
        [Fact]
        public async Task Feedback_UserName_Get_Should_Return_UserName_View()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.UserName();

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("Username");

        }
        [Fact]
        public async Task Feedback_UserName_Post_Should_Redirect_To_User_Email()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.UserName(new UserName{FullName = "Gopi"});

            //assert 
            var actionResult =   result as RedirectToActionResult;
             actionResult?.ActionName.Should().Be("UserEmail");

        }

        [Fact]
        public async Task Feedback_UserName_Post_Should_Show_Name_Required_User_Message()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();
            var userName = new UserName {FullName = ""};
            sut.ModelState.AddModelError("","Please enter name");

            //act
            var result = await sut.UserName(userName);

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("Username");
            viewResult?.ViewData.ModelState.IsValid.Should().BeFalse();
            viewResult?.ViewData.ModelState.ErrorCount.Should().BeGreaterOrEqualTo(1);

        }
        #endregion

        #region Email
        [Fact]
        public async Task Feedback_UserEmail_Get_Should_Return_UserEmail_View()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.UserEmail();

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("UserEmail");

        }
        [Fact]
        public async Task Feedback_UserEmail_Post_Should_Redirect_To_User_UserPostcode()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.UserEmail(new UserEmail() { Email = "Gopi@yahoo.com" });

            //assert 
            var actionResult = result as RedirectToActionResult;
            actionResult?.ActionName.Should().Be("UserPostcode");

        }

        [Fact]
        public async Task Feedback_Email_Post_Should_Show_Email_Required_User_Message()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();
            var userEmail = new UserEmail() { Email = "" };
            sut.ModelState.AddModelError("", "Please enter email");

            //act
            var result = await sut.UserEmail(userEmail);

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("UserEmail");
            viewResult?.ViewData.ModelState.IsValid.Should().BeFalse();
            viewResult?.ViewData.ModelState.ErrorCount.Should().BeGreaterOrEqualTo(1);
        }
        #endregion

        #region Postcode
        [Fact]
        public async Task Feedback_UserPostcode_Get_Should_Return_UserPostcode_View()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.UserPostcode();

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("UserPostcode");

        }
        [Fact]
        public async Task Feedback_UserPostcode_Post_Should_Redirect_To_User_UserAddress()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.UserPostcode(new UserPostcode() { Postcode = "DA7 6QJ" });

            //assert 
            var actionResult = result as RedirectToActionResult;
            actionResult?.ActionName.Should().Be("UserAddress");

        }

        [Fact]
        public async Task Feedback_UserPostcode_Post_Should_Show_Postcode_Required_User_Message()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();
            var userPostcode = new UserPostcode() { Postcode = "" };
            sut.ModelState.AddModelError("", "Please enter postcode");

            //act
            var result = await sut.UserPostcode(userPostcode);

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("UserPostcode");
            viewResult?.ViewData.ModelState.IsValid.Should().BeFalse();
            viewResult?.ViewData.ModelState.ErrorCount.Should().BeGreaterOrEqualTo(1);
        }
        #endregion

        #region UserAddress
        [Fact]
        public async Task Feedback_UserAddress_Get_Should_Return_UserAddress_View()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.UserAddress(new UserPostalAddress() { ListAddresses = new List<SelectListItem> { new SelectListItem { Text = "aa", Value = "1" } } });

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("UserAddress");

        }
        [Fact]
        public async Task Feedback_UserAddress_Post_Should_Redirect_To_User_HappyLevelLight()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.UserAddress(new  UserPostalAddress(){ListAddresses = new List<SelectListItem>{new SelectListItem{Text = "aa", Value = "1"}}});

            //assert 
            var actionResult = result as RedirectToActionResult;
            actionResult?.ActionName.Should().Be("HappyLevelLight");

        }

        [Fact]
        public async Task Feedback_UserAddress_Post_Should_Show_Address_Required_User_Message()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();
            var userAddress = new UserPostalAddress();
            sut.ModelState.AddModelError("", "Please enter postcode");

            //act
            var result = await sut.UserAddress(userAddress);

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("UserAddress");
            viewResult?.ViewData.ModelState.IsValid.Should().BeFalse();
            viewResult?.ViewData.ModelState.ErrorCount.Should().BeGreaterOrEqualTo(1);
        }
        #endregion

        #region HappyLevelLight
        [Fact]
        public async Task Feedback_HappyLevelLight_Get_Should_Return_HappyLevelLight_View()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.HappyLevelLight();

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("HappyLevelLight");

        }
        [Fact]
        public async Task Feedback_HappyLevelLight_Post_Should_Redirect_To_User_BrightnessScore()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.HappyLevelLight();

            //assert 
            var actionResult = result as RedirectToActionResult;
            actionResult?.ActionName.Should().Be("BrightnessScore");

        }

        [Fact]
        public async Task Feedback_HappyLevelLight_Post_Should_Show_Required_User_Message()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();
            sut.ModelState.AddModelError("", "Please select Yes or No");

            //act
            var result = await sut.HappyLevelLight();

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("HappyLevelLight");
            viewResult?.ViewData.ModelState.IsValid.Should().BeFalse();
            viewResult?.ViewData.ModelState.ErrorCount.Should().BeGreaterOrEqualTo(1);
        }
        #endregion

        #region UserAddress
        [Fact]
        public async Task Feedback_BrightnessScore_Get_Should_Return_BrightnessScore_View()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.BrightnessScore(new BrightnessScore() { ListBrightnessScores = new List<SelectListItem> { new SelectListItem { Text = "Good", Value = "1" } } });

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("BrightnessScore");

        }
        [Fact]
        public async Task Feedback_BrightnessScore_Post_Should_Redirect_To_CheckYourAnswer()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.BrightnessScore(new BrightnessScore() { ListBrightnessScores = new List<SelectListItem> { new SelectListItem { Text = "Good", Value = "1" } } });

            //assert 
            var actionResult = result as RedirectToActionResult;
            actionResult?.ActionName.Should().Be("CheckYourAnswer");

        }
       
        #endregion

        #region CheckFeedbackAndSubmit
        [Fact]
        public async Task Feedback_CheckAnswer_Get_Should_Return_CheckYourAnswers_View()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.CheckYourAnswer();

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("CheckYourAnswers");

        }

        #endregion

        #region Email
        [Fact]
        public async Task Feedback_Compelete_Get_Should_Return_CompleteFeedback_View()
        {
            // Arrange
            var sut = GetFeedbackControllerObject();

            //act
            var result = await sut.CompleteFeedback();

            //assert 
            var viewResult = result as ViewResult;
            viewResult?.ViewName.Should().Be("Complete");

        }
        
        #endregion
    }
}
