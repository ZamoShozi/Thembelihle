using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("user")]
    [Microsoft.EntityFrameworkCore.Index(nameof(email), Name = "customer_email_uindex", IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(phone_number), Name = "customer_phone_number_uindex", IsUnique = true)]
    public partial class user
    {
        public user()
        {
            bookings = new HashSet<booking>();
            payments = new HashSet<payment>();
        }

        [Key]
        public int id { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string name { get; set; } = null!;
        [StringLength(256)]
        [Unicode(false)]
        public string surname { get; set; } = null!;
        [StringLength(13)]
        [Unicode(false)]
        public string? phone_number { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string? email { get; set; }
        public int blocked { get; set; }
        [MaxLength(512)]
        public byte[] password_hash { get; set; } = null!;
        [MaxLength(512)]
        public byte[] password_salt { get; set; } = null!;
        public int role { get; set; }
        public int image { get; set; }

        [ForeignKey(nameof(image))]
        [InverseProperty(nameof(Image.users))]
        public virtual Image imageNavigation { get; set; } = null!;
        [ForeignKey(nameof(role))]
        [InverseProperty("users")]
        public virtual role roleNavigation { get; set; } = null!;
        [InverseProperty("customer")]
        public virtual address? address { get; set; }
        [InverseProperty(nameof(booking.customerNavigation))]
        public virtual ICollection<booking> bookings { get; set; }
        [InverseProperty(nameof(payment.customer))]
        public virtual ICollection<payment> payments { get; set; }
    }
}
