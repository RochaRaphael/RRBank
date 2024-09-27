using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RRBank.Domain.Database
{
    public class Client
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Manager))]
        public int ManagerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public DateTime Register { get; set; }
        public string CelNumber { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public Manager Manager { get; set; }
        [JsonIgnore]
        public List<Account> Accounts { get; set; }
        [JsonIgnore]
        public List<RequestCancellation> RequestCancellation { get; set; }
    }
}
