using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IAuditoryRepository? _auditoryRepository;
        private IClientRepository? _clientRepository;
        private IDetailInspectionRepository? _detailInspectionRepository;
        private IDetailsDiagnosticRepository? _detailsDiagnosticRepository;
        private IDiagnosticRepository? _diagnosticRepository;
        private IInspectionRepository? _inspectionRepository;
        private IInvoiceRepository? _invoiceRepository;
        private IOrderDetailsRepository? _orderDetailsRepository;
        private IRolRepository? _rolRepository;
        private IServiceOrderRepository? _serviceOrderRepository;
        private ISparePartRepository? _sparePartRepository;
        private ISpecializationRepository? _specialization;
        private IStateRepository? _stateRepository;
        private ITypeServiceRepository? _typeServiceRepository;
        private IUserRepository? _userRepository;
        private IUserSpecializationRepository? _userSpecializationRepository;
        private IVehicleRepository? _vehicleRepository;





        private readonly AutoTallerDbContext _context;

        public UnitOfWork(AutoTallerDbContext context)
        {
            _context = context;
        }





        public IAuditoryRepository Auditory
        {
            get
            {
                if (_auditoryRepository == null)
                {
                    _auditoryRepository = new AuditoryRepository(_context);
                }
                return _auditoryRepository;
            }
        }


        public IClientRepository Client
        {
            get
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(_context);
                }
                return _clientRepository;
            }
        }


        public IDetailInspectionRepository DetailInspection
        {
            get
            {
                if (_detailInspectionRepository == null)
                {
                    _detailInspectionRepository = new DetailInspectionRepository(_context);
                }
                return _detailInspectionRepository;
            }
        }


        public IDetailsDiagnosticRepository DetailsDiagnostic
        {
            get
            {
                if (_detailsDiagnosticRepository == null)
                {
                    _detailsDiagnosticRepository = new DetailsDiagnosticRepository(_context);
                }
                return _detailsDiagnosticRepository;
            }
        }



        public IDiagnosticRepository Diagnostic
        {
            get
            {
                if (_diagnosticRepository == null)
                {
                    _diagnosticRepository = new DiagnosticRepository(_context);
                }
                return _diagnosticRepository;
            }
        }

        public IInspectionRepository Inspection
        {
            get
            {
                if (_inspectionRepository == null)
                {
                    _inspectionRepository = new InspectionRepository(_context);
                }
                return _inspectionRepository;
            }
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }


    }
}