using RRBank.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRBank.Application.Model.ModelOut
{
    public class ClientListPaginatedOut
    {
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? LastClientId { get; set; }
        public List<Client> ClientList { get; set; } = new List<Client>();
    }
}
