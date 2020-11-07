using Athena.Models;

namespace Athena.Repositories
{
    public interface IPoolRepository
    {
        Pool Get(int id);
        Pool GetByDevice(string deviceId);
    }
}