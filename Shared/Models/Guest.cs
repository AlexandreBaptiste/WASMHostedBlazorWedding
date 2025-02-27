﻿namespace Wedding.Shared.Models
{
    /// <summary>
    /// Guests 
    /// Code first model
    /// </summary>
    public class Guest
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Firstname { get; set; }

        public string? Status { get; set; }

        public string? RelatedTo { get; set; }

        public string? Relation { get; set; }
    }
}