using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Razor.model{
    [Table("Sản Phẩm")]
    public class Product{
        [Key]
        [Column("Mã Sản Phẩm")]
        public int MaSanPham{set;get;} //
        [Required]
        [Column("Tên Sản Phẩm",TypeName ="nvarchar")]     
        [StringLength(255,MinimumLength =5,ErrorMessage ="Tên chỉ dài từ 5 đến 255")]
        [DisplayName("Tên Sản Phẩm")]      
        public String Name{set;get;}     //
        [Required(ErrorMessage ="Không được để rỗng giá")]
        [Column("Giá")]
        [Display(Name ="Giá")] 
        public int Price{set;get;}       //
        [Display(Name ="Ảnh")]
        public byte[] Anh1{set;get;}     //
        [Required]
        [Column("Số Lượng")]
        [Display(Name ="Số Lượng")]
        public int SoLuong{set;get;}     //
        [Required]
        [Column("Đã Bán")]
        [Display(Name ="Đã Bán")]
        public int sold{set;get;}   //
        [Required]
        [Column("Mã Kho")]
        public int MaKho{set;get;}       //
        public Kho kho{set;get;}       //

        public override bool Equals(object obj)
        {
            Product s = obj as Product;
            if(s == null){
                return false;
            }
            if(Name.Equals(s.Name)){
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

    }
}