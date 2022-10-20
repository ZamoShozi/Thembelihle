using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("room")]
    [Microsoft.EntityFrameworkCore.Index(nameof(id), Name = "room_id_uindex", IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(number), Name = "room_number_uindex", IsUnique = true)]
    public partial class room
    {
        public room()
        {
            booked_rooms = new HashSet<booked_room>();
        }

        [Key]
        public int id { get; set; }
        public int? type_id { get; set; }
        public byte? number { get; set; }
        public byte? status { get; set; }

        [ForeignKey(nameof(type_id))]
        [InverseProperty(nameof(room_type.rooms))]
        public virtual room_type? type { get; set; }
        [InverseProperty(nameof(booked_room.room))]
        public virtual ICollection<booked_room> booked_rooms { get; set; }
    }
}
