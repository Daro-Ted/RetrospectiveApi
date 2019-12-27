using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveApi.Models
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Participants { get; set; }
        public string Date { get; set; }
        public ICollection<FeedbackDto> Feedbacks { get; set; } = new List<FeedbackDto>();
    }
}
