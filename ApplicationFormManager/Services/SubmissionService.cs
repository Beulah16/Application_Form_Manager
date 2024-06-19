using ApplicationFormManager.ApplicationFormManager.Data;
using ApplicationFormManager.ApplicationFormManager.DTOs;
using ApplicationFormManager.ApplicationFormManager.Interfaces;
using ApplicationFormManager.ApplicationFormManager.Mappers;
using ApplicationFormManager.ApplicationFormManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormManager.ApplicationFormManager.Services
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
    }
}