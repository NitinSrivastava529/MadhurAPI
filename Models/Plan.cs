using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models
{

    public class Plan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string file { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string type { get; set; }

    }
}
