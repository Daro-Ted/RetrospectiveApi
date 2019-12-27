using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveApi.Entities
{
    public class RetropsectiveProject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(240)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Summary { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Participants { get;  set; }
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    }
}
