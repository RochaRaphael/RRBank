﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRBank.Application.Model.ModelIn
{
    public class ClientListPaginatedIn
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public int? LastClientId { get; set; }
    }
}
