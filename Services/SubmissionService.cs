using ApplicationFormManager.Data;
using ApplicationFormManager.DTOs;
using ApplicationFormManager.Interfaces;
using ApplicationFormManager.Mappers;
using ApplicationFormManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormManager.Services
{
    public class SubmissionService(AppDbContext dbContext) : ISubmissionService
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<List<Submission>> GetSubmissions()
        {
            return await _dbContext.Submissions.ToListAsync();

        }

        public async Task<Submission?> GetSubmissionById(Guid Id)
        {
            var submission = await _dbContext.Submissions.FindAsync(Id);

            if (submission == null) return null;

            return submission;
        }

        public async Task<Submission?> PostSubmission(CreateSubmissionRequest requestDto)
        {
            var applicationForm = await _dbContext.ApplicationForms.FindAsync(requestDto.ApplicationFormId);

            if (applicationForm == null) return null;

            var submission = requestDto.ToSubmissionModel();

            _dbContext.Submissions.Add(submission);
            await _dbContext.SaveChangesAsync();

            return submission;
        }

        public Task<Submission?> UpdateSubmission(Guid Id, CreateSubmissionRequest requestDto)
        {
            throw new NotImplementedException();
        }
        public Task<Submission?> DeleteSubmission(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}