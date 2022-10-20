using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("booked_extra")]
    [Microsoft.EntityFrameworkCore.Index(nameof(booking_id), Name = "booked_extra_booking_id_uindex", IsUnique = true)]
    public partial class booked_extra
    {
        [Key]
        public int id { get; set; }
        public int? extra_id { get; set; }
        public int? booking_id { get; set; }

        [ForeignKey(nameof(booking_id))]
        [InverseProperty("booked_extra")]
        public virtual booking? booking { get; set; }
        [ForeignKey(nameof(extra_id))]
        [InverseProperty("booked_extras")]
        public virtual extra? extra { get; set; }
    }
}
