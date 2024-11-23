using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models
{
    public class RewardMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public int AutoId { get; set; }
        [Column(TypeName ="varchar(20)")]
        public string? MemberId { get; set; }
        [Column(TypeName = "varchar(5)")]
        public string? level { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string? Remark { get; set; }
        public string? file_path { get; set; }
        public char IsApproved { get; set; } = 'N';
        public DateTime? CreationDate { get; set; }= DateTime.UtcNow;
    }
}
