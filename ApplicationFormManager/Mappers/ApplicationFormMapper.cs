using ApplicationFormManager.ApplicationFormManager.DTOs;
using ApplicationFormManager.ApplicationFormManager.Models;

namespace ApplicationFormManager.ApplicationFormManager.Mappers
{
    public static class ApplicationFormMapper
    {
        public static ApplicationForm ToApplicationFormModel(this CreateApplicationFormRequest request)
        {
            return new ApplicationForm
            {
                Title = request.Title,
                Description = request.Description,
                PersonalInputFields = request.PersonalInputFields,
                CustomQuestionFields = request.CustomQuestionFields,
            };
        }
    }
}