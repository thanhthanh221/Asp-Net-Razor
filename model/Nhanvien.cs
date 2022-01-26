using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Razor.model{
    [Table("Nhân Viên")]
    public class NhanVien{
        [Key]
        [Column("Mã Nhân Viên")] 
        public int MaNV{set;get;} //
        [Column("Họ Tên",TypeName ="nvarchar")]
        [StringLength(50)]
        [Required(ErrorMessage ="Không được Null")]
        [Display(Name ="Họ Và Tên")]
        [ModelBinder(BinderType = typeof(NhanVien_Name_Binding))]
        public string HoTen{set;get;}     //
        [Required(ErrorMessage ="Không được để trống")]
        [Display(Name ="Mã Số CMND")]
        public string CMND{set;get;}       //
        [Required(ErrorMessage ="Không được để trống")]
        [Display(Name ="Ngày Sinh")]
        [DataType(DataType.Date)]
        public DateTime? NgaySinh{set;get;}   //
        [Required(ErrorMessage ="Không được để trống")]
        [Column("Số Điện Thoại")]
        [Phone(ErrorMessage ="Phải là số điện thoại")]
        [StringLength(10,MinimumLength =10,ErrorMessage ="Số Điện thoại phải có 10 chữ số")]
        public string SDT{set;get;}          //
        [Column("Ảnh")]
        [Display(Name ="Ảnh")]
        public Byte[] Anh{set;get;}
        public List<HoaDon> HoaDons{set;get;}

        public override bool Equals(object obj)
        {
            NhanVien nhanVien = obj as NhanVien;
            if(nhanVien == null){
                return false;
            }
            return this.CMND.Equals(nhanVien.CMND);
        }
        public override int GetHashCode()
        {
            return this.CMND.GetHashCode();
        }
    }
}