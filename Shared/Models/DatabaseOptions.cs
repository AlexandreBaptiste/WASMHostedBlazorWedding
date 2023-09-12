namespace Wedding.Shared.Models
{
    /// <summary>
    /// Options for DB
    /// ON/OFFLINE
    /// PostreSQL/Mongo/MYSQL
    /// For now :
    /// Offline - PostreSQL
    /// </summary>
    public class DatabaseOptions
    {
        public const string DbOptions = "DatabaseOptions";

        public bool Online { get; set; }

        public string? Type { get; set; }
    }
}