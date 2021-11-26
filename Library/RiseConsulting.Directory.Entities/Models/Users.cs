using RiseConsulting.Directory.Entities.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiseConsulting.Directory.Entities.Models
{
    [Table("Users")]
    public class Users : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}