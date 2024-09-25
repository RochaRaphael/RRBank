using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRBank.Domain.Database
{
    public class Account
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsActive { get; set; }

        public Client Client { get; set; }
    }
}
