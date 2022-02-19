using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Razor.model{
    public class AppUser: IdentityUser{
        [Column("Tiền trong tài khoản")]
        [Display(Name = "Tiền trong tài khoản")]

        public double Monney{set;get;}
        [Display(Name ="Số Tiền Tiêu")]
        [Column("Tiền đã tiêu")]

        public double Monney_Use{set;get;}
        [Display(Name ="số đơn hàng đã đặt")]
        [Column("Số đơn hàng")]
        public int Count_DonHang{set;get;}
        public List<HoaDon> hoaDons{set;get;}
        public List<New> news{set;get;}
    }
}