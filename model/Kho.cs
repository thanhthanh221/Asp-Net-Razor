using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Razor.model{
    [Table("Kho")]
    public class Kho{
        [Key]
        [Column("Mã Kho")]
        public int MaKho{set;get;}
        [Required]
        [Display(Name = "Tên Kho")]
        [Column("Tên Kho",TypeName ="nvarchar")]
        [StringLength(50)]
        public string TenKho{set;get;}
        public virtual List<Product> Products{set;get;}
    }
}