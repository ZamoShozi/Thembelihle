using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("booked_room")]
    public partial class booked_room
    {
        public int room_id { get; set; }
        [Key]
        public int booking_id { get; set; }

        [ForeignKey(nameof(booking_id))]
        [InverseProperty("booked_room")]
        public virtual booking booking { get; set; } = null!;
        [ForeignKey(nameof(room_id))]
        [InverseProperty("booked_rooms")]
        public virtual room room { get; set; } = null!;
    }
}
