using System.Threading.Tasks;
using Feedback.Services.FeedbackService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Feedback.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IFeedbackService _feedbackService;
        public AdminController(ILogger<AdminController> logger,
                                  IFeedbackService feedbackService
                                  )
        {
            _logger = logger;
            _feedbackService = feedbackService;

        }
        public async Task<IActionResult> Index()
        {
            var feedback = await _feedbackService.GetAllFeedbacks();
            _logger.LogCritical("Retrieve all the feedback");
            return View("Index", feedback);
        }
    }
}