using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MadhurAPI.Models
{
    public class KycDocument
    {
        [Key]
        [Column(TypeName = "bigint")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutoId { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string MemberId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string file { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string type { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
