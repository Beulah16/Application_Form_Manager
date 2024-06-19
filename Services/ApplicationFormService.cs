using ApplicationFormManager.Data;
using ApplicationFormManager.DTOs;
using ApplicationFormManager.Interfaces;
using ApplicationFormManager.Mappers;
using ApplicationFormManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormManager.Services
{
    public class ApplicationFormService(AppDbContext dbContext) : IApplicationFormService
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<List<ApplicationForm>> GetForms()
        {
            return await _dbContext.ApplicationForms.ToListAsync();
        }

        public async Task<ApplicationForm?> GetFormById(Guid Id)
        {
            var applicationForm = await _dbContext.ApplicationForms.FindAsync(Id);

            if (applicationForm == null) return null;

            return applicationForm;
        }

        public async Task<ApplicationForm> PostForm(CreateApplicationFormRequest requestDto)
        {
            var applicationForm = requestDto.ToApplicationFormModel();

            _dbContext.ApplicationForms.Add(applicationForm);
            await _dbContext.SaveChangesAsync();

            return applicationForm;
        }

        public async Task<ApplicationForm?> UpdateForm(Guid Id, CreateApplicationFormRequest requestDto)
        {
            var applicationForm = await _dbContext.ApplicationForms.FindAsync(Id, requestDto);

            if (applicationForm == null) return null;

            applicationForm.Title = requestDto.Title;
            applicationForm.Description = requestDto.Description;
            applicationForm.CustomQuestionFields = requestDto.CustomQuestions;

            await _dbContext.SaveChangesAsync();

            return applicationForm;
        }

        public async Task<ApplicationForm?> DeleteForm(Guid Id)
        {
            var applicationForm = await _dbContext.ApplicationForms.FindAsync(Id);
            if (applicationForm == null) return null;

            _dbContext.ApplicationForms.Remove(applicationForm);
            await _dbContext.SaveChangesAsync();

            return applicationForm;
        }
    }
}