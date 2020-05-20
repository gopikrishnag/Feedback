using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.Services.FeedbackService
{
    public interface IFeedbackService
    {
        Task<IQueryable<Models.Feedback.Feedback>> GetAllFeedbacks();
       
        Task<bool> AddFeedback(Models.Feedback.Feedback feedback);
    }
}
