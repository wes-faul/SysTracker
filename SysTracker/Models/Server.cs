namespace SysTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Server")]

    [DisplayName("Server")]
    public partial class Server
    {
        [Key]
        [Column(Order = 0)]
        public Guid ServerID { get; set; }

        [Column(Order = 1)]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        //for navigation through EF
        public virtual ICollection<Link> Links { get; set; }
    }
}
