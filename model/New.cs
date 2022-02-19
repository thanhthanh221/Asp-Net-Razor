using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Razor.model{
    [Table("Thông Tin Mới")]
    public class New{
        [Key]
        [Required]
        public int ID{set;get;}
        [Required]
        [Column("Tiêu Đề")]
        [Display(Name = "Tiêu Đề")]
        public string title{set;get;}
        [Required]
        [Display(Name = "Nội Dung")]
        [Column("Nội Dung")]
        public string Content{set;get;}
        [Required]
        [Column("Ngày Tạo")]
        public DateTime Date_Created{set;get;}
        public string Id_User{set;get;}
        public readonly AppUser appUser;

    }
}