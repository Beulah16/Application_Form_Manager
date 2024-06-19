using ApplicationFormManager.Helpers;

namespace ApplicationFormManager.DTOs
{
    public class CreateSubmissionRequest
    {
        public Guid ApplicationFormId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IdNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public List<QuestionAnswer> AdditionalQuestions { get; set; } = [];
    }
}