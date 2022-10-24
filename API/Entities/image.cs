using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("image")]
    [Microsoft.EntityFrameworkCore.Index(nameof(id), Name = "image_id_uindex", IsUnique = true)]
    public partial class image
    {
        [Key]
        public int id { get; set; }
        public byte[] data { get; set; } = null!;
        [StringLength(256)]
        [Unicode(false)]
        public string type { get; set; } = null!;

        public virtual room_type typeNavigation { get; set; } = null!;
    }
}
