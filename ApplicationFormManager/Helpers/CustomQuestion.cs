using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApplicationFormManager.ApplicationFormManager.Helpers
{
    public class CustomQuestion
    {
        [Required]
        public string QuestionType { get; set; } = string.Empty;

        [JsonIgnore]
        public QuestionType Type
        {
            get { return (QuestionType)Enum.Parse(typeof(QuestionType), QuestionType); }
            set { QuestionType = value.ToString(); }
        }

        [Required]
        public string QuestionText { get; set; } = string.Empty;
        public List<string>? Choices { get; set; }
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