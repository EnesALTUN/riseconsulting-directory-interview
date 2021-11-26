﻿using RiseConsulting.Directory.Entities.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiseConsulting.Directory.Entities.Models
{
    [Table("Company")]
    public class Company : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CompanyId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}