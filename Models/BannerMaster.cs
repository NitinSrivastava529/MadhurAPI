using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models
{

    public class BannerMaster
    {
        [Key]
        [Column(TypeName = "bigint")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutoId { get; set; }

        [Column(TypeName ="varchar(200)")]
        public string Banner { get; set; }       
    }
}
