using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Razor.model{
    [Table("Sản Phẩm")]
    public class Product{
        [Key]
        [Column("Mã Sản Phẩm")]
        public int MaSanPham{set;get;}
        [Required]
        [Column("Tên Sản Phẩm",TypeName ="nvarchar")]     
        [StringLength(255,MinimumLength =5,ErrorMessage ="Tên chỉ dài từ 5 đến 255")]
        public String Name{set;get;}
        [Required(ErrorMessage ="Không được để rỗng giá")]
        [Column("Giá")]
        public int Price{set;get;}
        [Required]
        [Column("Số Lượng")]
        public int SoLuong{set;get;}
        [Required]
        [Column("Mã Kho")]
        public int MaKho{set;get;}
        public Kho kho{set;get;}

    }
}