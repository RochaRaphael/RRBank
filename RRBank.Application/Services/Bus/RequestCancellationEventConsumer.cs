using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RRBank.Application.Model;
using RRBank.Infra;

namespace RRBank.Application.Services.Bus
{
    internal class RequestCancellationEventConsumer : IConsumer<RequestCancellationEvent>
    {
        private readonly ILogger<RequestCancellationEventConsumer> _logger;
        private readonly DataContext _context;
        public RequestCancellationEventConsumer(ILogger<RequestCancellationEventConsumer> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Consume(ConsumeContext<RequestCancellationEvent> context)
        {
            var message = context.Message;
            _logger.LogInformation($"Processing cancellation request Id:{message.Id} Name:{message.name}");

            var request = await _context.RequestCancellation.FirstOrDefaultAsync( x => x.Id == message.Id );
            if ( request != null)
            {
                request.Status = 2;
                request.ProcessedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            
            _logger.LogInformation($"Cancellation request processed Id:{message.Id} Name:{message.name}");
        }
    }
}
