using RiseConsulting.Directory.Entities.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiseConsulting.Directory.Entities.Models
{
    [Table("DirectoryUsers")]
    public class DirectoryUsers : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DirectoryUsersId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(255)]
        public Guid CompanyId { get; set; }

        [Required]
        [MaxLength(255)]
        public Guid UserId { get; set; }

        public DateTime CreatedDate { get; set; }


        public virtual Company Company { get; set; }
        public virtual Users Users { get; set; }
    }
}