using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("promotion")]
    [Microsoft.EntityFrameworkCore.Index(nameof(code), Name = "promotion_code_uindex", IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(id), Name = "promotion_id_uindex", IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(type), Name = "promotion_type_uindex", IsUnique = true)]
    public partial class promotion
    {
        public promotion()
        {
            bookings = new HashSet<booking>();
        }

        [Key]
        public int id { get; set; }
        public int code { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string type { get; set; } = null!;
        public double percentage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime expiry_date { get; set; }

        [InverseProperty(nameof(booking.promotionNavigation))]
        public virtual ICollection<booking> bookings { get; set; }
    }
}
