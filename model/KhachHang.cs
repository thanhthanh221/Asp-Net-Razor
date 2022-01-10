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
        [StringLength(30,ErrorMessage ="Tên Không được dài quá")]
        public string HoTen{set;get;}
        public int CMND{set;get;}
        [EmailAddress(ErrorMessage ="Địa chỉ phải là email")]
        public string email{set;get;}
        public virtual List<HoaDon> HoaDons{set;get;}
        [Required(ErrorMessage ="Không được để trống")]
        [Column("Số Điện Thoại")]
        [Phone(ErrorMessage ="Phải là số điện thoại")]
        [StringLength(10,MinimumLength =10,ErrorMessage ="Số Điện thoại phải có 10 chữ số")]
        public string SĐT{set;get;}
    }
}