using CarService.Features.ShopInterface.Services.Events;

namespace CarService.Server.WebAPI.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic(nameof(WarrantAddedEvent))]
        public WarrantAddedEvent WarrantAdded([EventMessage] WarrantAddedEvent warrantAddedEvent) => warrantAddedEvent;

        [Subscribe]
        [Topic(nameof(WarrantRemovedEvent))]
        public WarrantRemovedEvent WarrantRemoved([EventMessage] WarrantRemovedEvent warrantRemovedEvent) => warrantRemovedEvent;

        [Subscribe]
        [Topic(nameof(WarrantUpdatedEvent))]
        public WarrantUpdatedEvent WarrantUpdated([EventMessage] WarrantUpdatedEvent warrantUpdatedEvent) => warrantUpdatedEvent;
    }
}
