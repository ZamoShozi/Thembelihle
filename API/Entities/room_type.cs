using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("room_type")]
    public partial class room_type
    {
        public room_type()
        {
            rooms = new HashSet<room>();
        }

        [Key]
        public int id { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string? type { get; set; }
        public int? number_of_beds { get; set; }
        public double? price { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string? description { get; set; }
        public int? guest { get; set; }
        public int? image { get; set; }

        [ForeignKey(nameof(image))]
        [InverseProperty(nameof(Image.room_types))]
        public virtual Image? imageNavigation { get; set; }
        [InverseProperty(nameof(room.type))]
        public virtual ICollection<room> rooms { get; set; }
    }
}
