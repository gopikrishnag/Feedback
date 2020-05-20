using Feedback.Repository.Entities;
using Feedback.Repository.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IGenericRepository<FeedbackDto> _feedbackRepository;
        private readonly ILogger<FeedbackService> _logger;
        public FeedbackService(IGenericRepository<FeedbackDto> feedbackRepository,
                               ILogger<FeedbackService> logger)
        {
            _feedbackRepository = feedbackRepository;
            _logger = logger;
        }
        public async Task<IQueryable<Models.Feedback.Feedback>> GetAllFeedbacks()
        {
            try
            {
                var feedbacks = _feedbackRepository.GetAll();

                var listFeedbacks = feedbacks.Select(f => new Models.Feedback.Feedback
                {
                    FeedbackId = f.FeedbackId,
                    Name = f.Name,
                    Email = f.Email,
                    HomePostcode = f.HomePostcode,
                    HomeAddress = f.HomeAddress,
                    HappyWithLevelOfLighting = f.HappyWithLevelOfLighting,
                    LevelOfBrightness = f.LevelOfBrightness,
                    CreatedOn = f.CreatedOn
                }).ToList();

                return listFeedbacks.AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllFeedbacks - Error - {ex}");
                return null;
            }

        }

        public async Task<bool> AddFeedback(Models.Feedback.Feedback feedback)
        {
            try
            {
                var dbFeedback = new FeedbackDto
                {
                    Name = feedback.Name,
                    Email = feedback.Email,
                    HomePostcode = feedback.HomePostcode,
                    HomeAddress = feedback.HomeAddress,
                    HappyWithLevelOfLighting = feedback.HappyWithLevelOfLighting,
                    LevelOfBrightness = feedback.LevelOfBrightness,
                    CreatedOn = feedback.CreatedOn
                };
                await _feedbackRepository.Add(dbFeedback);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddFeedback - Error - {ex}");
                return false;
            }

        }
    }


}
