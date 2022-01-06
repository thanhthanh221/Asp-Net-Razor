using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Razor.model{
    [Table("Nguời Mua")]
    public class KhachHang{
        [Key]
        public int ID{set;get;}
        [Column("Họ Tên",TypeName ="nvarchar")]
        [StringLength(255)]
        public string HoTen{set;get;}
        public int CMND{set;get;}
        public virtual List<HoaDon> HoaDons{set;get;}
    }
}