using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Razor.model{
    [Table("Thuộc tính sản phẩm")]
    public class Product_Attribute{
        [Key]
        public int ID_Table{set;get;}
        [Column("Mã Sản Phẩm")]
        public int Product_ID{set;get;}
        [Column("Mã giá trị thuộc tính")]
        public int Attributes_Value_ID{set;get;}
        public Product product{set;get;}
        public virtual Attributes_Value attributes_Value{set;get;}


    }
}