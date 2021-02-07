namespace SysTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Link")]
    public partial class Link
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage= "Server is required")]
        public Guid ServerID { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage= "System is required")]
        public Guid SystemsID { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessage = "Server Type is required")]
        public Guid ServerTypeID { get; set; }

        public virtual Server Server { get; set; }
        public virtual Systems System { get; set; }
        public virtual ServerType ServerType { get; set; }
    }
}
