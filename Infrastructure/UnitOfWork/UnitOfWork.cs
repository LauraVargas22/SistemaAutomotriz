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
        private ISpecializationRepository? _specializationRepository;
        private IStateRepository? _stateRepository;
        private ITypeServiceRepository? _typeServiceRepository;
        private ITypeVehicleRepository? _typeVehicleRepository;
        private IUserRepository? _userRepository;
        private IUserRolRepository? _userRolRepository;
        private IUserSpecializationRepository? _userSpecializationRepository;
        private IVehicleRepository? _vehicleRepository;
        private IRefreshTokenRepository? _refreshToken;

        private readonly AutoTallerDbContext _context;

        public UnitOfWork(AutoTallerDbContext context)
        {
            _context = context;
        }

        public IAuditoryRepository AuditoryRepository
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

        public IClientRepository ClientRepository
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

        public IDetailInspectionRepository DetailInspectionRepository
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

        public IDetailsDiagnosticRepository DetailsDiagnosticRepository
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

        public IDiagnosticRepository DiagnosticRepository
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

        public IInspectionRepository InspectionRepository
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

        public IInvoiceRepository InvoiceRepository
        {
            get
            {
                if (_invoiceRepository == null)
                {
                    _invoiceRepository = new InvoiceRepository(_context);
                }
                return _invoiceRepository;
            }
        }

        public IOrderDetailsRepository OrderDetailsRepository
        {
            get
            {
                if (_orderDetailsRepository == null)
                {
                    _orderDetailsRepository = new OrderDetailsRepository(_context);
                }
                return _orderDetailsRepository;
            }
        }

        public IRolRepository RolRepository
        {
            get
            {
                if (_rolRepository == null)
                {
                    _rolRepository = new RolRepository(_context);
                }
                return _rolRepository;
            }
        }

        public IServiceOrderRepository ServiceOrderRepository
        {
            get
            {
                if (_serviceOrderRepository == null)
                {
                    _serviceOrderRepository = new ServiceOrderRepository(_context);
                }
                return _serviceOrderRepository;
            }
        }

        public ISparePartRepository SparePartRepository
        {
            get
            {
                if (_sparePartRepository == null)
                {
                    _sparePartRepository = new SparePartRepository(_context);
                }
                return _sparePartRepository;
            }
        }

        public ISpecializationRepository SpecializationRepository
        {
            get
            {
                if (_specializationRepository == null)
                {
                    _specializationRepository = new SpecializationRepository(_context);
                }
                return _specializationRepository;
            }
        }

        public IStateRepository StateRepository
        {
            get
            {
                if (_stateRepository == null)
                {
                    _stateRepository = new StateRepository(_context);
                }
                return _stateRepository;
            }
        }

        public ITypeServiceRepository TypeServiceRepository
        {
            get
            {
                if (_typeServiceRepository == null)
                {
                    _typeServiceRepository = new TypeServiceRepository(_context);
                }
                return _typeServiceRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IUserRolRepository UserRolRepository
        {
            get
            {
                if (_userRolRepository == null)
                {
                    _userRolRepository = new UserRolRepository(_context);
                }
                return _userRolRepository;
            }
        }

        public IUserSpecializationRepository UserSpecializationRepository
        {
            get
            {
                if (_userSpecializationRepository == null)
                {
                    _userSpecializationRepository = new UserSpecializationRepository(_context);
                }
                return _userSpecializationRepository;
            }
        }

        public IVehicleRepository VehicleRepository
        {
            get
            {
                if (_vehicleRepository == null)
                {
                    _vehicleRepository = new VehicleRepository(_context);
                }
                return _vehicleRepository;
            }
        }

        public IRefreshTokenRepository RefreshTokenRepository
        {
            get
            {
                if (_refreshToken == null)
                {
                    _refreshToken = new RefreshTokenRepository(_context);
                }
                return _refreshToken;
            }
        }

        public ITypeVehicleRepository TypeVehicleRepository
        {
            get
            {
                if (_typeVehicleRepository == null)
                {
                    _typeVehicleRepository = new TypeVehicleRepository(_context);
                }
                return _typeVehicleRepository;
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
