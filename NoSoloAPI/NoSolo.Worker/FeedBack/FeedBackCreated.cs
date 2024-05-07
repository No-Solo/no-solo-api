using MassTransit;
using Microsoft.Extensions.Logging;
using NoSolo.Contracts.Dtos.FeedBack;

namespace NoSolo.Worker.FeedBack;

public class FeedBackCreated(ILogger<FeedBackCreated> logger) : IConsumer<FeedBackDto>
{
    public Task Consume(ConsumeContext<FeedBackDto> context)
    {
        logger.LogInformation(context.Message.ToString());
        return Task.CompletedTask;
    }
}