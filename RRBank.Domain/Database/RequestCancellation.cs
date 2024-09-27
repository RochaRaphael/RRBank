using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RRBank.Domain.Database
{
    public class RequestCancellation
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public int Status { get; set; }

        [JsonIgnore]
        public Client Client { get; set; }
    }
}
