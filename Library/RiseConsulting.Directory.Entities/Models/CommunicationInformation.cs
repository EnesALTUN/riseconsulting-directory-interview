using RiseConsulting.Directory.Entities.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiseConsulting.Directory.Entities.Models
{
    [Table("CommunicationInformation")]
    public class CommunicationInformation : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CommunicationInformationId { get; set; }

        [Required]
        [MaxLength(255)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Location { get; set; }

        [Required]
        [MaxLength(255)]
        public string Detail { get; set; }

        [Required]
        [MaxLength(255)]
        public Guid DirectoryUsersId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public virtual DirectoryUsers DirectoryUsers { get; set; }
    }
}