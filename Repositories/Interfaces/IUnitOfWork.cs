using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        AppDbContext DbContext { get; }
        IAccountRepository AccountRepository { get; }
        IRatingRepository RatingRepository { get; }
        IPodRepository PodRepository { get; }
        ILocationRepository LocationRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        IRatingCommentRepository CommentRepository { get; }

        public Task<int> SaveChangeAsync();
    }
}
