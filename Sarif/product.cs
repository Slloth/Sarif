namespace Sarif
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        [Key]
        public int idProduct { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(200)]
        public string description { get; set; }

        [Required]
        [StringLength(10)]
        public string serialNumber { get; set; }

        public int number { get; set; }

        public int price { get; set; }

        [Required]
        public byte[] img { get; set; }

        public int idCategorie { get; set; }

        public virtual categories categories { get; set; }
    }
}
