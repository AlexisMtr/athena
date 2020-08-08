using System.Linq;
using Microsoft.EntityFrameworkCore;
using Athena.Configuration;
using Athena.Models;

namespace Athena.Repositories.SQL
{
    public class PoolRepository : IPoolRepository
    {
        private readonly AthenaContext context;

        public PoolRepository(AthenaContext context)
        {
            this.context = context;
        }

        public Pool Get(int id)
        {
            return context.Pools
                .Include(e => e.Device.Configuration)
                .FirstOrDefault(e => e.Id.Equals(id));
        }

        public Pool GetByDevice(string deviceId)
        {
            return context.Pools
                .Include(e => e.Device.Configuration)
                .FirstOrDefault(e => e.Device.DeviceId.Equals(deviceId));
        }
    }
}
