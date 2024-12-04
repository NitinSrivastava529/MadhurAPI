using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models
{

    public class YoutubeVideo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string code { get; set; }
    }
}
