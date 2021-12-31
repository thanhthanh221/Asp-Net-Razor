using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Razor.model{
     public class Blog{
        [Key]
        public int ID{set;get;}
        [StringLength(255)]
        [Required]
        [Column(TypeName ="nvarchar")]
        public string Title{set;get;}
        [DataType(DataType.Date)]
        public DateTime Created{set;get;}
        [Column(TypeName ="ntext")]
        public string Content{set;get;}

    }
}