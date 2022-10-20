using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Microsoft.EntityFrameworkCore.Index(nameof(id), Name = "roles_id_uindex", IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(type), Name = "roles_type_uindex", IsUnique = true)]
    public partial class role
    {
        public role()
        {
            users = new HashSet<user>();
        }

        [Key]
        public int id { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string type { get; set; } = null!;

        [InverseProperty(nameof(user.roleNavigation))]
        public virtual ICollection<user> users { get; set; }
    }
}
