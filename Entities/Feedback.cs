using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetrospectiveApi.Entities
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(350)]
        public string Name { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Types { get; set; }
        [ForeignKey("ProjectId")]
        public RetropsectiveProject Project { get; set; }
        public int ProjectId { get; set; }
    }
}