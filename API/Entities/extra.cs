using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("extra")]
    [Microsoft.EntityFrameworkCore.Index(nameof(name), Name = "extra_name_uindex", IsUnique = true)]
    public partial class extra
    {
        public extra()
        {
            booked_extras = new HashSet<booked_extra>();
        }

        [Key]
        public int id { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string name { get; set; } = null!;
        public double? price { get; set; }
        [StringLength(1)]
        [Unicode(false)]
        public string availability { get; set; } = null!;
        public int? quantity { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string? description { get; set; }

        [InverseProperty(nameof(booked_extra.extra))]
        public virtual ICollection<booked_extra> booked_extras { get; set; }
    }
}
