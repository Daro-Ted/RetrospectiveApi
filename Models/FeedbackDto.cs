﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrospectiveApi.Models
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public string Types { get; set; }
    }
}
