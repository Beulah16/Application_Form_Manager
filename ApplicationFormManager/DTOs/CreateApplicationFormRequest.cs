using ApplicationFormManager.ApplicationFormManager.Helpers;

namespace ApplicationFormManager.ApplicationFormManager.DTOs
{
    public class CreateApplicationFormRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<InputField> PersonalInputFields { get; set; } = [];
        public List<CustomQuestion> CustomQuestionFields { get; set; } = [];
    }
}