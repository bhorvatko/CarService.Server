using CarService.Server.Core.Events;
using HotChocolate.Subscriptions;

namespace CarService.Server.WebAPI.GraphQL
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly ITopicEventSender topicEventSender;
        private readonly IHttpContextAccessor httpContextAccessor;

        public EventDispatcher(ITopicEventSender topicEventSender, IHttpContextAccessor httpContextAccessor)
        {
            this.topicEventSender = topicEventSender;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task DispatchEvent<TEvent>(TEvent message) where TEvent : DomainEvent
        {
            message.InitiatorSessionId = GetInitiatorSessionId();

            await topicEventSender.SendAsync(typeof(TEvent).Name, message, CancellationToken.None);
        }

        private string GetInitiatorSessionId()
        {
            string? sessionId = httpContextAccessor.HttpContext!.Request.Headers["SessionId"];

            if (sessionId == null) throw new Exception("Client did not provide session ID in request header.");

            return sessionId;
        }
    }
}
