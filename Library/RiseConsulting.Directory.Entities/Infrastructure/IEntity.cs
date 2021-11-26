using System;

namespace RiseConsulting.Directory.Entities.Infrastructure
{
    public interface IEntity
    {
        public DateTime CreatedDate { get; set; }
    }
}