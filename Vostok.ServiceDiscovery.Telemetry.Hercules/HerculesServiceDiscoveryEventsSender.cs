using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.ServiceDiscovery.Telemetry.Event;
using Vostok.ServiceDiscovery.Telemetry.EventsSender;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules
{
    /// <summary>
    /// An implementation of <see cref="IServiceDiscoveryEventsSender"/> that saves incoming events as <see cref="HerculesEvent"/>s using an instance of <see cref="IHerculesSink"/>.
    /// </summary>
    [PublicAPI]
    public class HerculesServiceDiscoveryEventsSender : IServiceDiscoveryEventsSender
    {
        private readonly HerculesServiceDiscoveryEventsSenderSettings settings;

        public HerculesServiceDiscoveryEventsSender([NotNull] HerculesServiceDiscoveryEventsSenderSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Send(ServiceDiscoveryEvent serviceDiscoveryEvent) =>
            settings.HerculesSink.Put(settings.StreamName,
                builder => HerculesServiceDiscoveryEventsBuilder.Build(serviceDiscoveryEvent, builder));
    }
}