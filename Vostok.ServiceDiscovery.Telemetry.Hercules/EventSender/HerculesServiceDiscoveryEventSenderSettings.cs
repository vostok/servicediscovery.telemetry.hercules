using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.EventSender
{
    [PublicAPI]
    public class HerculesServiceDiscoveryEventSenderSettings
    {
        public HerculesServiceDiscoveryEventSenderSettings([NotNull] IHerculesSink herculesSink) =>
            HerculesSink = herculesSink ?? throw new ArgumentNullException(nameof(herculesSink));

        [NotNull]
        public IHerculesSink HerculesSink { get; }

        [NotNull]
        public string StreamName { get; set; } = "serviceDiscovery_event";
    }
}