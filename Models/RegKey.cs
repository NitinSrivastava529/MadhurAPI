using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models
{
    public class RegKey
    {
        [Key]
        [Column(TypeName = "bigint")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuotId { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string Key { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
