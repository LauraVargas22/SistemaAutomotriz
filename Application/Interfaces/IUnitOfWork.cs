using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAuditoryRepository AuditoryRepository { get; }
        IClientRepository ClientRepository { get; }
        IDetailInspectionRepository DetailInspectionRepository { get; }
        IDetailsDiagnosticRepository DetailsDiagnosticRepository { get; }
        IDiagnosticRepository DiagnosticRepository { get; }
        IInspectionRepository InspectionRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IRolRepository RolRepository { get; }
        IServiceOrderRepository ServiceOrderRepository { get; }
        ISparePartRepository SparePartRepository { get; }
        ISpecializationRepository SpecializationRepository { get; }
        IStateRepository StateRepository { get; }
        ITypeServiceRepository TypeServiceRepository { get; }
        IUserRepository UserRepository { get; }
        IUserRolRepository UserRolRepository { get; }
        IUserSpecializationRepository UserSpessializationRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        Task<int> SaveAsync();
    }
}