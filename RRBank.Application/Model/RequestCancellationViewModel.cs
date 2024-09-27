using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRBank.Application.Model
{
    public class RequestCancellationViewModel
    {
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public ProcessingStatus Status { get; set; }
    }

    public enum ProcessingStatus
    {
        Pending = 1,
        Processed = 2,
        Denied = 3
    }

    internal sealed record RequestCancellationEvent(Guid Id, string name);
}
