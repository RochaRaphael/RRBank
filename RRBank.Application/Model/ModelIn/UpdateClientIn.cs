using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRBank.Application.Model.ModelIn
{
    public class UpdateClientIn
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? CelNumber { get; set; }
    }
}
