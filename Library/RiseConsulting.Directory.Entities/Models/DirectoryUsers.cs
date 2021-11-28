using RiseConsulting.Directory.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public Guid CompanyId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }


        [JsonIgnore]
        public virtual ICollection<CommunicationInformation> CommunicationInformation { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }

        [JsonIgnore]
        public virtual Users User { get; set; }
    }
}