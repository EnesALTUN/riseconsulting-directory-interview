using System;
using System.ComponentModel.DataAnnotations;

namespace RiseConsulting.Directory.Entities.Infrastructure
{
    public interface IEntity
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
    }
}