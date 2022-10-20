using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("booking")]
    public partial class booking
    {
        [Key]
        public int id { get; set; }
        public int amount { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string status { get; set; } = null!;
        public int number_guests { get; set; }
        public int customer { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? check_in { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? check_out { get; set; }
        public int card { get; set; }
        public int breakfast { get; set; }
        public int promotion { get; set; }

        [ForeignKey(nameof(card))]
        [InverseProperty(nameof(payment.bookings))]
        public virtual payment cardNavigation { get; set; } = null!;
        [ForeignKey(nameof(customer))]
        [InverseProperty(nameof(user.bookings))]
        public virtual user customerNavigation { get; set; } = null!;
        [ForeignKey(nameof(promotion))]
        [InverseProperty("bookings")]
        public virtual promotion promotionNavigation { get; set; } = null!;
        [InverseProperty("booking")]
        public virtual booked_extra? booked_extra { get; set; }
        [InverseProperty("booking")]
        public virtual booked_room booked_room { get; set; } = null!;
        [InverseProperty("bookingNavigation")]
        public virtual booking_message booking_message { get; set; } = null!;
        [InverseProperty("bookingNavigation")]
        public virtual feedback feedback { get; set; } = null!;
    }
}
