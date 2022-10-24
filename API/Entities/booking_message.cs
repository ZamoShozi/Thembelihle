using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("booking_message")]
    [Microsoft.EntityFrameworkCore.Index(nameof(booking), Name = "booking_message_booking_uindex", IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(id), Name = "booking_message_id_uindex", IsUnique = true)]
    public partial class booking_message
    {
        [Key]
        public int id { get; set; }
        public int booking { get; set; }
        [Unicode(false)]
        public string message { get; set; } = null!;
        public int read { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime date { get; set; }

        [ForeignKey(nameof(booking))]
        [InverseProperty("booking_message")]
        public virtual booking bookingNavigation { get; set; } = null!;
    }
}
