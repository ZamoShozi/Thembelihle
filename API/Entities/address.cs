using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    [Table("address")]
    [Microsoft.EntityFrameworkCore.Index(nameof(customer_id), Name = "address_customer_id_uindex", IsUnique = true)]
    public partial class address
    {
        [Key]
        public int id { get; set; }
        public int? customer_id { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string? country { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string? state { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string? city { get; set; }
        public int? zip { get; set; }

        [ForeignKey(nameof(customer_id))]
        [InverseProperty(nameof(user.address))]
        public virtual user? customer { get; set; }
    }
}
