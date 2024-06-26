using ApplicationFormManager.ApplicationFormManager.DTOs;
using ApplicationFormManager.ApplicationFormManager.Models;

namespace ApplicationFormManager.ApplicationFormManager.Interfaces
{
    public interface ISubmissionService
    {
        Task<List<Submission>> GetSubmissions();
        Task<Submission?> GetSubmissionById(Guid Id);
        Task<Submission?> PostSubmission(CreateSubmissionRequest requestDto);
    }
}