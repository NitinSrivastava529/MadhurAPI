using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models
{
    public class RewardDistributor
    {
        [Key]
        [Column(TypeName = "bigint")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutoId { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string MemberId { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string DistributorId { get; set; }
        public string Level { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
