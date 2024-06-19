using ApplicationFormManager.DTOs;
using ApplicationFormManager.Models;

namespace ApplicationFormManager.Interfaces
{
    public interface ISubmissionService
    {
        Task<List<Submission>> GetSubmissions();
        Task<Submission?> GetSubmissionById(Guid Id);
        Task<Submission?> PostSubmission(CreateSubmissionRequest requestDto);
        Task<Submission?> UpdateSubmission(Guid Id, CreateSubmissionRequest requestDto);
        Task<Submission?> DeleteSubmission(Guid Id);
    }
}