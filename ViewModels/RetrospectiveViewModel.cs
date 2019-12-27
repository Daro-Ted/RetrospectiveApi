using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveApi.ViewModels
{
    public class RetrospectiveViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Participants { get; private set; }
        public string Date { get; set; }
        public ICollection<FeedbackViewModel> Feedbacks { get; set; } = new List<FeedbackViewModel>();
    }
}
