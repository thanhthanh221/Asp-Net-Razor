using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Razor.model{
    [Table("Hóa Đơn")]
    public class HoaDon{
        [Key]
        [Column("Mã Hóa Đơn")]
        public int MaHoaDon{set;get;}
        [Required]
        [Column("Tiền")]
        [Display(Name ="giá")]
        public int Money{set;get;}
        [Required]
        public int ID_Shiper{set;get;}
        public Shipper shipper{set;get;}
        // Id người mua hóa đơn này
        public string Id_User{set;get;}
        public readonly  AppUser appUser;
        public readonly List<Product_Sell_Bill> product_Sell_Bill;

    }
}