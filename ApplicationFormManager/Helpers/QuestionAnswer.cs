using System.ComponentModel.DataAnnotations;

namespace ApplicationFormManager.ApplicationFormManager.Helpers
{
    public class QuestionAnswer
    {
        [Required]
        public string Question { get; set; } = string.Empty;
        [Required]
        public string Answer { get; set; } = string.Empty;
    }
}