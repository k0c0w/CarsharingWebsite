using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Migrations.CarsharingApp;

namespace Features.CarManagement.Admin.Commands.CreateModel
{
    public class CreateCarModelSaveImageResponseConsumer : IConsumer<CarModelImageSaveResultDto>
    {
        private readonly CarsharingContext carsharingContext;
        private readonly ILogger<string> logger;
        public CreateCarModelSaveImageResponseConsumer(ILogger<string> logger, CarsharingContext carsharingContext)
        {
            this.logger = logger;
            this.carsharingContext = carsharingContext;
        }

        public async Task Consume(ConsumeContext<CarModelImageSaveResultDto> context)
        {
            var message = context.Message;

            if (message.Success)
            {
                var carModel = await carsharingContext.CarModels.FirstOrDefaultAsync(x => x.Id == message.CarModelId);
                if (carModel != null)
                {
                    carModel.ImageUrl = message.Url;
                    await carsharingContext.SaveChangesAsync();
                }
                logger.Log(LogLevel.Information, "occured here 2");
            }
            else
                logger.Log(LogLevel.Information, "error occured");
        }
    }
}
