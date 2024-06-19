using ApplicationFormManager.ApplicationFormManager.Helpers;

namespace ApplicationFormManager.ApplicationFormManager.DTOs
{
    public class CreateSubmissionRequest
    {
        public Guid ApplicationFormId { get; set; }
        public List<PersonalInformation> PersonalDetails { get; set; } = [];
        public List<QuestionAnswer> AdditionalDetails { get; set; } = [];
    }
}