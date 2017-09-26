namespace TaskTracking.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tasktracking.t_memeber")]
    public partial class t_memeber
    {
        [Key]
        public int Member { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        public int GroupId { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        [StringLength(250)]
        public string CreateUser { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        [StringLength(250)]
        public string UpdateUser { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDate { get; set; }
    }
}
