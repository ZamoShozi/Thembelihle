using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("extra_type")]
    [Microsoft.EntityFrameworkCore.Index(nameof(id), Name = "extra_type_id_uindex", IsUnique = true)]
    public partial class extra_type
    {
        [Key]
        public int id { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string? type { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string? description { get; set; }
        [Column(TypeName = "money")]
        public decimal? price { get; set; }
    }
}
