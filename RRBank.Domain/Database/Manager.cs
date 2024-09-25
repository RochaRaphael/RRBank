using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RRBank.Domain.Database
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public DateTime Register { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public IList<Client> Clients { get; set; }
    }
}
