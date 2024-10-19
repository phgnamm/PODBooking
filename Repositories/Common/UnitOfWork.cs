using Repositories.Interfaces;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IAccountRepository _accountRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IPodRepository _podRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IRatingCommentRepository _ratingCommentRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRewardPointsRepository _rewardPointsRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingServiceRepository _bookingServiceRepository;   

        public UnitOfWork(AppDbContext dbContext, IAccountRepository accountRepository, 
            IRatingRepository ratingRepository, 
            IPodRepository podRepository, ILocationRepository locationRepository, 
            IDeviceRepository deviceRepository,IRatingCommentRepository ratingCommentRepository
            ,IServiceRepository serviceRepository,IRewardPointsRepository rewardPointsRepository, IBookingRepository bookingRepository, IBookingServiceRepository bookingServiceRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _ratingRepository = ratingRepository;
            _podRepository = podRepository;
            _locationRepository = locationRepository;
            _deviceRepository = deviceRepository;
            _ratingCommentRepository = ratingCommentRepository;
            _serviceRepository = serviceRepository;
            _rewardPointsRepository = rewardPointsRepository;
            _bookingRepository = bookingRepository;
            _bookingServiceRepository = bookingServiceRepository;
        }

        public AppDbContext DbContext => _dbContext;
        public IAccountRepository AccountRepository => _accountRepository;
        public IRatingRepository RatingRepository => _ratingRepository;
        public IPodRepository PodRepository => _podRepository;
        public IRatingCommentRepository CommentRepository => _ratingCommentRepository;

        public ILocationRepository LocationRepository => _locationRepository;

        public IDeviceRepository DeviceRepository => _deviceRepository;

        public IServiceRepository ServiceRepository => _serviceRepository;

        public IBookingRepository BookingRepository => _bookingRepository;

        public IBookingServiceRepository BookingServiceRepository => _bookingServiceRepository;

        public IRewardPointsRepository RewardPointsRepository => _rewardPointsRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
