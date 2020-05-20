using System;
using System.Collections.Generic;
using Feedback.Helpers.CacheHelper;
using Feedback.Models.Constants;
using Feedback.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Feedback.Models;
using Feedback.Models.Settings;
using Feedback.Services.AddressService;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Feedback.Services.FeedbackService;

namespace Feedback.Web.Controllers
{
    public class FeedbackController : Controller
    {
        private const string FeedbackControllerName = "Feedback";

        private readonly ILogger<FeedbackController> _logger;
        private readonly ICacheHelpers _cache;
        private readonly IAddressService _addressService;
        private readonly IFeedbackService _feedbackService;
        private readonly WebSettings _webSettings;
        public FeedbackController(ILogger<FeedbackController> logger,
                                  ICacheHelpers cache,
                                  IAddressService addressService,
                                  IFeedbackService feedbackService,
                                      WebSettings webSettings)
        {
            _logger = logger;
            _cache = cache;
            _addressService = addressService;
            _feedbackService = feedbackService;
            _webSettings = webSettings;

        }


        public async Task<IActionResult> Index()
        {
            return View("Index");
        }

        #region UserName
        [HttpGet("name")]
        public async Task<IActionResult> UserName()
        {
            var userName = new UserName { FullName = await _cache.GetCache(FeedbackSession.FullName) };
            return ViewForUsernamePage(userName);
        }
        [HttpPost("name")]
        public async Task<IActionResult> UserName(UserName vm)
        {
            if (!ModelState.IsValid)
            {
                return ViewForUsernamePage(vm);
            }
            await _cache.SetCache(FeedbackSession.FullName, vm.FullName);
            return RedirectToAction("UserEmail");
        }

        private ViewResult ViewForUsernamePage(UserName vm)
        {
            vm.BackButton = new BackButton { ControllerName = FeedbackControllerName, ActionName = "Index" };
            return View("Username", vm);
        }

        #endregion

        #region UserEmail
        [HttpGet("email")]
        public async Task<IActionResult> UserEmail()
        {
            var userEmail = new UserEmail { Email = await _cache.GetCache(FeedbackSession.Email) };
            return ViewForUserEmailPage(userEmail);
        }
        [HttpPost("email")]
        public async Task<IActionResult> UserEmail(UserEmail vm)
        {
            if (!ModelState.IsValid)
            {
                return ViewForUserEmailPage(vm);
            }
            await _cache.SetCache(FeedbackSession.Email, vm.Email);
            return RedirectToAction("UserPostcode");
        }

        private ViewResult ViewForUserEmailPage(UserEmail vm)
        {
            vm.BackButton = new BackButton { ControllerName = FeedbackControllerName, ActionName = "UserName" };
            return View("UserEmail", vm);
        }

        #endregion

        #region UserPostcode
        [HttpGet("postcode")]
        public async Task<IActionResult> UserPostcode()
        {
            var userPostcode = new UserPostcode() { Postcode = await _cache.GetCache(FeedbackSession.Postcode) };
            return ViewForUserPostcodePage(userPostcode);
        }
        [HttpPost("postcode")]
        public async Task<IActionResult> UserPostcode(UserPostcode vm)
        {
            if (!ModelState.IsValid)
            {
                return ViewForUserPostcodePage(vm);
            }
            await _cache.SetCache(FeedbackSession.Postcode, vm.Postcode);
            return RedirectToAction("UserAddress");
        }

        private ViewResult ViewForUserPostcodePage(UserPostcode vm)
        {
            vm.BackButton = new BackButton { ControllerName = FeedbackControllerName, ActionName = "UserEmail" };
            return View("UserPostcode", vm);
        }

        #endregion

        #region UserAddress
        [HttpGet("address")]
        public async Task<IActionResult> UserAddress()
        {
            var userAddress = new UserPostalAddress();
            var userPostcode = await _cache.GetCache(FeedbackSession.Postcode);
            var addresses = await _addressService.GetAddresses(_webSettings.PostalAddressUrl, _webSettings.PostalAddressKey, userPostcode);
            if (!addresses.Any())
            {
                ModelState.AddModelError(nameof(userAddress.SelectedAddress), "Please either verify the entered postcode or Network Proxy and Internet Options --> Connection -->LAN Settings");
            }
            var selectedAddress = await _cache.GetCache(FeedbackSession.Address);

            userAddress.SelectedAddress = selectedAddress;
            userAddress.ListAddresses = FillAddressInDropdown(addresses);

            return ViewForUserAddressPage(userAddress);
        }
        [HttpPost("address")]
        public async Task<IActionResult> UserAddress(UserPostalAddress vm)
        {
            if (!ModelState.IsValid)
            {
                return ViewForUserAddressPage(vm);
            }
            await _cache.SetCache(FeedbackSession.Address, vm.SelectedAddress);
            return RedirectToAction("HappyLevelLight");
        }

        private ViewResult ViewForUserAddressPage(UserPostalAddress vm)
        {
            vm.BackButton = new BackButton { ControllerName = FeedbackControllerName, ActionName = "UserPostcode" };
            return View("UserAddress", vm);
        }

        private static List<SelectListItem> FillAddressInDropdown(List<PostalAddress> addresses)
        {
            var selectedListItem = new List<SelectListItem>();
            foreach (var address in addresses)
            {
                var street = string.Join(" ", address.Address);
                var fullAddress = $"{street},{address.Citytown},{address.Postcode}";
                selectedListItem.Add(new SelectListItem { Text = street, Value = fullAddress });
            }


            return selectedListItem;
        }

        #endregion

        #region HappyLevelLight
        [HttpGet("happy-level-light")]
        public async Task<IActionResult> HappyLevelLight()
        {
            var happyLevelLight = new HappyLevelLight();

            var lightLevel = await _cache.GetCache(FeedbackSession.HappyLevelLight);
            if (!string.IsNullOrEmpty(lightLevel))
            {
                int.TryParse(lightLevel, out var intLightLevel);
                happyLevelLight.HappyWithLevelOfLighting = intLightLevel;
            }


            return ViewForHappyLevelLightPage(happyLevelLight);
        }
        [HttpPost("happy-level-light")]
        public async Task<IActionResult> HappyLevelLight(HappyLevelLight vm)
        {
            if (!ModelState.IsValid)
            {
                return ViewForHappyLevelLightPage(vm);
            }
            await _cache.SetCache(FeedbackSession.HappyLevelLight, vm.HappyWithLevelOfLighting.ToString());
            return RedirectToAction("BrightnessScore");
        }

        private ViewResult ViewForHappyLevelLightPage(HappyLevelLight vm)
        {
            vm.BackButton = new BackButton { ControllerName = FeedbackControllerName, ActionName = "UserAddress" };
            return View("HappyLevelLight", vm);
        }

        #endregion

        #region BrightnessScore
        [HttpGet("brightness-score")]
        public async Task<IActionResult> BrightnessScore()
        {
            var brightnessScore = new BrightnessScore();
            var score = await _cache.GetCache(FeedbackSession.BrightnessScore);

            if (!string.IsNullOrEmpty(score))
            {
                brightnessScore.BringhtnessScoreValue = score;
            }

            brightnessScore.ListBrightnessScores = FillBrightnessScoreInDropdown();

            return ViewForUserBrightnessScorePage(brightnessScore);
        }
        [HttpPost("brightness-score")]
        public async Task<IActionResult> BrightnessScore(BrightnessScore vm)
        {
            if (!ModelState.IsValid)
            {
                return ViewForUserBrightnessScorePage(vm);
            }
            await _cache.SetCache(FeedbackSession.BrightnessScore, vm.BringhtnessScoreValue);
            return RedirectToAction("CheckYourAnswer");
        }

        private ViewResult ViewForUserBrightnessScorePage(BrightnessScore vm)
        {
            vm.BackButton = new BackButton { ControllerName = FeedbackControllerName, ActionName = "HappyLevelLight" };
            return View("BrightnessScore", vm);
        }

        private static List<SelectListItem> FillBrightnessScoreInDropdown()
        {
            var selectedListItem = new List<SelectListItem>();
            for (var i = 1; i < 11; i++)
            {
                string text;
                switch (i)
                {
                    case 1:
                        text = "1 very dark";
                        break;
                    case 10:
                        text = "10 very bright";
                        break;
                    default:
                        text = i.ToString();
                        break;
                }


                selectedListItem.Add(new SelectListItem { Text = text, Value = i.ToString() });
            }


            return selectedListItem;
        }

        #endregion


        #region CheckYourAnswer
        [HttpGet("check-your-answer")]
        public async Task<IActionResult> CheckYourAnswer()
        {
            return await ViewForUserCheckYourAnswerPage(true);
        }

        [HttpPost("check-your-answer")]
        public async Task<IActionResult> CheckYourAnswerSave()
        {
            var validate = await ValidateFeedback();

            if (!validate)
            {
                return await ViewForUserCheckYourAnswerPage(false);

            }

            var saveResult = await SaveFeedback();
            if (saveResult)
            {
                _logger.LogInformation("Feedback has been saved into database");
                await RemoveCacheFromMemory();
            }
            return RedirectToAction("CompleteFeedback");
        }

        private async Task<ViewResult> ViewForUserCheckYourAnswerPage(bool validate)
        {
            var vm = await GetValuesFromCache();
            vm.BackButton = new BackButton { ControllerName = FeedbackControllerName, ActionName = "HappyLevelLight" };
            if (!validate)
            {
                ModelState.AddModelError(nameof(vm.Confirm), "Please complete the all questions");
            }

            return View("CheckYourAnswers", vm);
        }

        private async Task<CheckYourAnswers> GetValuesFromCache()
        {
            var answer = new CheckYourAnswers
            {
                FullName = await _cache.GetCache(FeedbackSession.FullName),
                EmailAddress = await _cache.GetCache(FeedbackSession.Email),
                HomePostcode = await _cache.GetCache(FeedbackSession.Postcode),
                HomeAddress = await _cache.GetCache(FeedbackSession.Address),
                LevelOfBrightness = await _cache.GetCache(FeedbackSession.BrightnessScore),
                BackButton = new BackButton
                {
                    ControllerName = FeedbackControllerName,
                    ActionName = "BrightnessScore"
                }
            };

            var levelOfLight = await _cache.GetCache(FeedbackSession.HappyLevelLight);
            if (!string.IsNullOrEmpty(levelOfLight))
            {
                answer.HappyLevelOfLighting = levelOfLight == "1" ? "Yes" : "No";
            }

            return answer;
        }
        private async Task<bool> ValidateFeedback()
        {
            var answer = await GetValuesFromCache();
            if (string.IsNullOrEmpty(answer.FullName)) return false;
            if (string.IsNullOrEmpty(answer.EmailAddress)) return false;
            if (string.IsNullOrEmpty(answer.HomePostcode)) return false;
            if (string.IsNullOrEmpty(answer.HomeAddress)) return false;
            if (string.IsNullOrEmpty(answer.HappyLevelOfLighting)) return false;
            if (string.IsNullOrEmpty(answer.LevelOfBrightness)) return false;
            return true;
        }

        private async Task<bool> SaveFeedback()
        {
            var feedback = await GetValuesFromCache();
            var dbFeedback = new Models.Feedback.Feedback
            {
                Name = feedback.FullName,
                Email = feedback.EmailAddress,
                HomePostcode = feedback.HomePostcode,
                HomeAddress = feedback.HomeAddress,
                HappyWithLevelOfLighting =  feedback.HappyLevelOfLighting.ToLower() == "yes",
                LevelOfBrightness = int.Parse(feedback.LevelOfBrightness),
                CreatedOn = DateTime.Now
            };
            return await _feedbackService.AddFeedback(dbFeedback);
        }

        private async Task RemoveCacheFromMemory()
        {
            await _cache.RemoveCache(FeedbackSession.FullName);
            await _cache.RemoveCache(FeedbackSession.Email);
            await _cache.RemoveCache(FeedbackSession.Postcode);
            await _cache.RemoveCache(FeedbackSession.Address);
            await _cache.RemoveCache(FeedbackSession.HappyLevelLight);
            await _cache.RemoveCache(FeedbackSession.BrightnessScore);
             
        }
        #endregion


        #region CompleteFeedback
        [HttpGet("complete-feedback")]
        public async Task<IActionResult> CompleteFeedback()
        {
            var random = new Random();
            var complete = new Complete() { Reference = $"Apr-20-{random.Next(100, 500)}" };
            return View("Complete", complete);
        }


        #endregion

        #region MiscPages
        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
