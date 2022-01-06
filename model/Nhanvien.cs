using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Razor.model{
    [Table("Nhân Viên")]
    public class NhanVien{
        [Key]
        [Column("Mã Nhân Viên")]
        public int MaNV{set;get;}
        [Column("Họ Tên",TypeName ="nvarchar")]
        [StringLength(50)]
        [Required]
        public string HoTen{set;get;}
        [Required]
        public int CMND{set;get;}
        [Required]
        public DateTime NgaySinh{set;get;}
        [Required]
        [Column("Số Điện Thoại")]
        public int SDT{set;get;}
        public List<HoaDon> HoaDons{set;get;}

    }
}