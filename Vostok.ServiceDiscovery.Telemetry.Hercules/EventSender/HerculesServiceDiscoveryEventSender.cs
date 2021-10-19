using System;
using JetBrains.Annotations;
using Vostok.ServiceDiscovery.Telemetry.Event;
using Vostok.ServiceDiscovery.Telemetry.EventSender;


namespace Vostok.ServiceDiscovery.Telemetry.Hercules.EventSender
{
    [PublicAPI]
    public class HerculesServiceDiscoveryEventSender : IServiceDiscoveryEventSender
    {
        private readonly HerculesServiceDiscoveryEventSenderSettings settings;

        public HerculesServiceDiscoveryEventSender([NotNull] HerculesServiceDiscoveryEventSenderSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Send(ServiceDiscoveryEvent serviceDiscoveryEvent) =>
            settings.HerculesSink.Put(settings.StreamName,
                builder => HerculesServiceDiscoveryEventBuilder.Build(serviceDiscoveryEvent, builder));
    }
}