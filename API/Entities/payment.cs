using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("payment")]
    [Microsoft.EntityFrameworkCore.Index(nameof(card_number), Name = "payment_card_number_uindex", IsUnique = true)]
    public partial class payment
    {
        public payment()
        {
            bookings = new HashSet<booking>();
        }

        [Key]
        public int id { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string card_holder { get; set; } = null!;
        [StringLength(16)]
        [Unicode(false)]
        public string card_number { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime expiry_date { get; set; }
        public int? customer_id { get; set; }
        public int? cvv { get; set; }

        [ForeignKey(nameof(customer_id))]
        [InverseProperty(nameof(user.payments))]
        public virtual user? customer { get; set; }
        [InverseProperty(nameof(booking.cardNavigation))]
        public virtual ICollection<booking> bookings { get; set; }
    }
}
