using Athena.Models;

namespace Athena.Repositories
{
    public interface IAlarmRepository
    {
        void Add(Alarm alarm);
        void Remove(Alarm alarm);
        void SaveChanges();
    }
}
