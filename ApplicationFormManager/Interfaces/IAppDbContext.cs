using ApplicationFormManager.ApplicationFormManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormManager.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<ApplicationForm> ApplicationForms { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
