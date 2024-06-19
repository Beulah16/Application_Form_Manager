using System.ComponentModel.DataAnnotations;

namespace ApplicationFormManager.Helpers
{
    public class CustomQuestion
    {
        [Required]
        public string QuestionType { get; set; } = string.Empty;
        [Required]
        public string QuestionText { get; set; } = string.Empty;
        public List<string?> Choices { get; set; } = [];
    }

    public enum QuestionType
    {
        Paragraph, 
        YesNo, 
        Dropdown, 
        MultipleChoice, 
        Date, 
        Number
    }
}