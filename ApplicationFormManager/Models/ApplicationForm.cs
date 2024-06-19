using ApplicationFormManager.ApplicationFormManager.Helpers;

namespace ApplicationFormManager.ApplicationFormManager.Models
{
    public class ApplicationForm
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<InputField> PersonalInputFields { get; set; } = [];
        public List<CustomQuestion> CustomQuestionFields { get; set; } = [];

    }
}