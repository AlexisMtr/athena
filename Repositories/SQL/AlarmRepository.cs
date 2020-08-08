using Athena.Configuration;
using Athena.Models;

namespace Athena.Repositories.SQL
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly AthenaContext context;

        public AlarmRepository(AthenaContext context)
        {
            this.context = context;
        }

        public void Add(Alarm alarm)
        {
            context.Alarms.Add(alarm);
        }

        public void Remove(Alarm alarm)
        {
            context.Alarms.Remove(alarm);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
