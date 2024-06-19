using System.ComponentModel.DataAnnotations;

namespace ApplicationFormManager.ApplicationFormManager.Helpers
{
    public class InputField
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Label { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public bool IsInternal { get; set; }
        public bool IsHidden { get; set; }
    }
}