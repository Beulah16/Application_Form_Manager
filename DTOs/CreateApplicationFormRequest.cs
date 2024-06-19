using ApplicationFormManager.Helpers;
using ApplicationFormManager.Models;

namespace ApplicationFormManager.DTOs
{
    public class CreateApplicationFormRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<PersonalInformations> PersonalInfos { get; set; } = [];
        public List<CustomQuestion> CustomQuestions { get; set; } = [];
    }
}