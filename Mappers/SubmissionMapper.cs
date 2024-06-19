using ApplicationFormManager.DTOs;
using ApplicationFormManager.Models;

namespace ApplicationFormManager.Mappers
{
    public static class SubmissionMapper
    {
        public static Submission ToSubmissionModel(this CreateSubmissionRequest requestDto)
        {
            return new Submission
            {
                ApplicationFormId = requestDto.ApplicationFormId,
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
                PhoneNumber = requestDto.PhoneNumber,
                Nationality = requestDto.Nationality,
                CurrentResidence = requestDto.CurrentResidence,
                IdNumber = requestDto.IdNumber,
                DateOfBirth = requestDto.DateOfBirth,
                Gender = requestDto.Gender,
                AdditionalQuestions = requestDto.AdditionalQuestions,
            };
        }
    }
}