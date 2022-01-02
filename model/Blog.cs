using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Razor.model{
     public class Blog{
        [Key]
        public int ID{set;get;}
        [StringLength(255,MinimumLength =5,ErrorMessage ="{0} dài từ 5 dến 255")]
        [Required(ErrorMessage ="{0} phải nhập")]
        [Column(TypeName ="nvarchar")]
        [DisplayName("Tiêu đề")]
        public string Title{set;get;}
        [DataType(DataType.Date)]
        [DisplayName("Ngày Tạo")]
        [Required(ErrorMessage ="Ngày Tháng phải nhập")]
        public DateTime Created{set;get;}
        [Column(TypeName ="ntext")]
        [DisplayName("Nội Dung")]
        public string Content{set;get;}

    }
}