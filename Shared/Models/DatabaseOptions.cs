namespace Wedding.Shared.Models
{
    public class DatabaseOptions
    {
        public const string DbOptions = "DatabaseOptions";

        public bool Online { get; set; }

        public string? Type { get; set; }
    }
}