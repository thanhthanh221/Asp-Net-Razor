using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Razor.model{
    [Table("Hóa Đơn")]
    public class HoaDon{
        [Key]
        [Column("Mã Hóa Đơn")]
        public int MaHoaDon{set;get;}
        [Required]
        [Column("Mã Nhân Viên Bán")]
        public int MaNhanVienBan{set;get;}
        [Required]
        [Column("Người Mua")]
        public int NguoiMua{set;get;}
        [Required]
        public NhanVien nhanVien{set;get;}
        [Required]
        public KhachHang KhachHang{set;get;}
    }
}