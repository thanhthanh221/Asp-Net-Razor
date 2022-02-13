using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Razor.model{
    [Table("Giá trị thuộc tính")]
    public class Attributes_Value{
        [Required]
        [Key]
        public int ID{set;get;}
        [Required]
        [Column("Mã Thuộc tính")]
        public int Attributes_ID{set;get;}
        [Required]
        [Column("Thuộc Tính")]
        public string value{set;get;}
        public virtual List<Product_Attribute> product_Attributes{set;get;}
        public virtual Attributes Attributes{set;get;}

    }
}