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
        [Column("Tên Kho")]
        public int TenKho{set;get;}
        public virtual List<Product> Products{set;get;}
    }
}