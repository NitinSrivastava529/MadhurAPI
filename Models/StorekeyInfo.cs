using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models
{
   
    public class StorekeyInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint", Order = 0)]
        public int AutoId { get; set; }
        [Column(TypeName ="varchar(20)",Order =1)]
        public string? StoreId { get; set; }
        [Column(TypeName = "varchar(20)", Order = 1)]
        public string? MemberId { get; set; }
    }
}
