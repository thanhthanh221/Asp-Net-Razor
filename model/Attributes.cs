using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Razor.model{
    [Table("Thuộc Tính")]
    public class Attributes{
        [Required]
        [Key]
        public int ID{set;get;}
        [Required]
        [Column("Tên")]
        [Display(Name ="Thuộc Tính")]
        public string Name{set;get;}
        public List<Attributes_Value> attributes_Values{set;get;}
    }
}