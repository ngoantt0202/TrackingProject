namespace TaskTracking.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tasktracking.t_task")]
    public partial class t_task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [StringLength(250)]
        public string Subject { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        [Required]
        [StringLength(1)]
        public string Priority { get; set; }

        [Required]
        [StringLength(1)]
        public string Important { get; set; }

        [Required]
        [StringLength(1)]
        public string Type { get; set; }

        public int GroupId { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public int Done { get; set; }

        [Required]
        [StringLength(250)]
        public string Assign { get; set; }

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
