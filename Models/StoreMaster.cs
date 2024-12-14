using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models
{
    public class StoreMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public int AutoId { get; set; }
        [Column(TypeName ="varchar(20)")]
        public string StoreId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string StoreName { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? Mobile { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string? Address { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? State { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? City { get; set; }
        [Column(TypeName = "varchar(6)")]
        public string? PinCode { get; set; }
        public string? type { get; set; }
    }
}
