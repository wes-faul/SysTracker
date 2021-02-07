namespace SysTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServerType")]
    [DisplayName("Server Type")]

    public partial class ServerType
    {
        [Key]
        [Column(Order = 0)]
        public Guid ServerTypeID { get; set; }

        [Column(Order = 1)]
        [StringLength(20)]
        public string Name { get; set; }

        [Column(Order = 2)]
        [StringLength(1024)]
        public string Description { get; set; }

        public virtual ICollection<Link> Links { get; set; }
    }
}
