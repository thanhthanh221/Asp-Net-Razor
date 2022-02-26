using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Razor.model{
    [Table("Sản Phẩm Bán theo hóa đơn")]
    public class Product_Sell_Bill{
        [Key]
        [Required]
        public int ID{set;get;}
        // Mã Số của sản phẩm đó
        [Required]
        public int Product_ID{set;get;}
        [Display(Name = "Mã Hóa Đơn")]
        [Column("Mã Hóa Đơn")]
        public int MaHoaDon{set;get;}
        [Column("Số Lượng")]
        [Display(Name = "Số Lượng")]
        public int SoLuong{set;get;}
        public HoaDon hoaDon{set;get;}
        public Product product{set;get;}

    }
}