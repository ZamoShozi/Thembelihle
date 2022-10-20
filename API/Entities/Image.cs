using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("Image")]
    [Microsoft.EntityFrameworkCore.Index(nameof(id), Name = "Image_id_uindex", IsUnique = true)]
    public partial class Image
    {
        public Image()
        {
            room_types = new HashSet<room_type>();
            users = new HashSet<user>();
        }

        [Key]
        public int id { get; set; }
        public byte[] data { get; set; } = null!;

        [InverseProperty(nameof(room_type.imageNavigation))]
        public virtual ICollection<room_type> room_types { get; set; }
        [InverseProperty(nameof(user.imageNavigation))]
        public virtual ICollection<user> users { get; set; }
    }
}
