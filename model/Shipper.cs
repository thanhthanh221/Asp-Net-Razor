using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace Razor.model{
    [Table("Hãng Giao Hàng")]
    public class Shipper{
        [Key]
        public int ID{set;get;}
        [Required]
        [Display(Name ="Tên Hãng")]
        [Column("Tên Hãng")]
        public string Name{set;get;}
        [Required]
        [Column("giá")]
        [Display(Name ="giá")]
        public int Price{set;get;}
        public List<HoaDon> hoaDon{set;get;}
        
    }
}