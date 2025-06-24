using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context;
            if (context == null)
                return result;

            LogChanges(context);
            return result;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
                return result;

            LogChanges(context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void LogChanges(DbContext context)
        {
            var auditLogs = new List<Auditory>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    auditLogs.Add(new Auditory
                    {
                        EntityName = entry.Entity.GetType().Name,
                        ChangeType = entry.State.ToString(),
                        ChangedBy = "SystemUser"
                    });
                }
            }

            if (auditLogs.Any())
            {
                context.Set<Auditory>().AddRange(auditLogs);
            }
        }
    }
}