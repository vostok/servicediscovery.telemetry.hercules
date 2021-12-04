using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.ServiceDiscovery.Telemetry.Event;
using Vostok.ServiceDiscovery.Telemetry.EventSender;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.EventSender
{
    /// <summary>
    /// An implementation of <see cref="IServiceDiscoveryEventSender"/> that saves incoming events as <see cref="HerculesEvent"/>s using an instance of <see cref="IHerculesSink"/>.
    /// </summary>
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