using ApplicationFormManager.ApplicationFormManager.Data;
using ApplicationFormManager.ApplicationFormManager.DTOs;
using ApplicationFormManager.ApplicationFormManager.Interfaces;
using ApplicationFormManager.ApplicationFormManager.Mappers;
using ApplicationFormManager.ApplicationFormManager.Models;
using ApplicationFormManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormManager.ApplicationFormManager.Services
{
    public class ApplicationFormService : IApplicationFormService
    {
        private readonly AppDbContext _dbContext;

        public ApplicationFormService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
            var applicationForm = await _dbContext.ApplicationForms.FindAsync(Id);

            if (applicationForm == null) return null;

            applicationForm.Title = requestDto.Title;
            applicationForm.Description = requestDto.Description;
            applicationForm.CustomQuestionFields = requestDto.CustomQuestionFields;

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