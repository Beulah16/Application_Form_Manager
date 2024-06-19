using ApplicationFormManager.DTOs;
using ApplicationFormManager.Models;

namespace ApplicationFormManager.Mappers
{
    public static class ApplicationFormMapper
    {
        public static ApplicationForm ToApplicationFormModel(this CreateApplicationFormRequest request)
        {
            return new ApplicationForm
            {
                Title = request.Title,
                Description = request.Description,
                PersonalInputFields = request.PersonalInfos,
                CustomQuestionFields = request.CustomQuestions,
            };
        }
    }
}