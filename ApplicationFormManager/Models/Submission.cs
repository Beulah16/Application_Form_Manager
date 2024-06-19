using ApplicationFormManager.ApplicationFormManager.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApplicationFormManager.ApplicationFormManager.Models
{
    public class Submission
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public List<PersonalInformation> PersonalDetails { get; set; } = [];
        [Required]
        public List<QuestionAnswer> AdditionalDetails { get; set; } = [];
        [Required]
        public Guid ApplicationFormId { get; set; }
        [JsonIgnore]
        public ApplicationForm? ApplicationForm { get; set; }

    }
}