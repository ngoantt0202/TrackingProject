namespace TaskTracking.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tasktracking.t_group")]
    public partial class t_group
    {
        [Key]
        public int GroupId { get; set; }

        [Required]
        [StringLength(250)]
        public string GroupName { get; set; }

        [Required]
        [StringLength(1)]
        public string GroupType { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        public int Members { get; set; }

        [StringLength(250)]
        public string CreateUser { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        [StringLength(250)]
        public string CreateUpdate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDate { get; set; }
    }
}
