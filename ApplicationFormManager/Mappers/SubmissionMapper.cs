using ApplicationFormManager.ApplicationFormManager.DTOs;
using ApplicationFormManager.ApplicationFormManager.Models;

namespace ApplicationFormManager.ApplicationFormManager.Mappers
{
    public static class SubmissionMapper
    {
        public static Submission ToSubmissionModel(this CreateSubmissionRequest requestDto)
        {
            return new Submission
            {
                ApplicationFormId = requestDto.ApplicationFormId,
                PersonalDetails = requestDto.PersonalDetails,
                AdditionalDetails = requestDto.AdditionalDetails,
            };
        }
    }
}