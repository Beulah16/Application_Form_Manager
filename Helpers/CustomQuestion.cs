using System.ComponentModel.DataAnnotations;

namespace ApplicationFormManager.Models
{
    public class CustomQuestion
    {
        [Required]
        public string QuestionType { get; set; } = string.Empty;
        [Required]
        public string QuestionText { get; set; } = string.Empty;
        public List<string>? Choices { get; set; }
    }

    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse,
        OpenEnded
    }
}