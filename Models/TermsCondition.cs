using System.ComponentModel.DataAnnotations.Schema;

namespace MadhurAPI.Models
{
    public class TermsCondition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "bigint")]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string content { get; set; }
    }
}
