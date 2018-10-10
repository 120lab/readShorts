using System;
using readShorts.Models.Interfaces;

namespace readShorts.Models.CQRS
{
    public class Task : IEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted;
        public DateTime LastUpdated;
    }
}