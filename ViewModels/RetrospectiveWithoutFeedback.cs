using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveApi.ViewModels
{
    public class RetrospectiveWithoutFeedback
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime Date { get; set; }
        public string Participants { get; set; }
    }
}
