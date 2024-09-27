using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RRBank.Application.Services.Bus;

namespace RRBank.Application.Extensions
{
    public static class RabbitExtension
    {
        public static void AddRabbitMQService(this IServiceCollection services)
        {
            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.AddConsumer<RequestCancellationEventConsumer>();
                busConfiguration.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("rabbitmq", host =>
                    {
                        host.Username("user");
                        host.Password("1q2w3e4r@#$");
                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
    }
}
