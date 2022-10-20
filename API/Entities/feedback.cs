using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("feedback")]
    [Microsoft.EntityFrameworkCore.Index(nameof(booking), Name = "feedback_booking_uindex", IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(id), Name = "feedback_id_uindex", IsUnique = true)]
    public partial class feedback
    {
        [Key]
        public int id { get; set; }
        public int rating { get; set; }
        [Unicode(false)]
        public string message { get; set; } = null!;
        public int booking { get; set; }

        [ForeignKey(nameof(booking))]
        [InverseProperty("feedback")]
        public virtual booking bookingNavigation { get; set; } = null!;
    }
}
