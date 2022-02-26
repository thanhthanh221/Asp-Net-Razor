using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Razor.model{
    [Table("Danh Mục")]
    // Sản Phẩm có thể thuộc 1 hay nhiều danh mục
    public class Categories{
        [Key]
        [Required]
        public int ID {set;get;}
        [Display(Name = "Tên Danh Mục")]
        [Column("Tên Danh Mục",TypeName = "nvarchar")]
        [StringLength(50, MinimumLength = 3,ErrorMessage = " Tên danh mục không được quá dài hay quá ngắn")]
        public string Name{set;get;}
        public List<Product> products{set;get;}
        
    }
}