using ApplicationFormManager.ApplicationFormManager.DTOs;
using ApplicationFormManager.ApplicationFormManager.Models;

namespace ApplicationFormManager.ApplicationFormManager.Interfaces
{
    public interface IApplicationFormService
    {
        Task<List<ApplicationForm>> GetForms();
        Task<ApplicationForm?> GetFormById(Guid Id);
        Task<ApplicationForm> PostForm(CreateApplicationFormRequest requestDto);
        Task<ApplicationForm?> UpdateForm(Guid Id, CreateApplicationFormRequest requestDto);
        Task<ApplicationForm?> DeleteForm(Guid Id);
    }
}