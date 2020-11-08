namespace Athena.Dtos
{
    public class DeviceConfigurationDto
    {
        public bool IsPublished { get; set; }
        public string Version { get; set; }
        public long SleepModeStart { get; set; }
        public long PublicationDelay { get; set; }
        public long ConfigurationUpdateCheckDelay { get; set; }
    }
}
