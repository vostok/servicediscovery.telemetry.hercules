using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.EventSender
{
    /// <summary>
    /// Represents configuration of <see cref="HerculesServiceDiscoveryEventSender"/>.
    /// </summary>
    [PublicAPI]
    public class HerculesServiceDiscoveryEventSenderSettings
    {
        public HerculesServiceDiscoveryEventSenderSettings([NotNull] IHerculesSink herculesSink) =>
            HerculesSink = herculesSink ?? throw new ArgumentNullException(nameof(herculesSink));

        /// <summary>
        /// <see cref="IHerculesSink"/> used to emit events.
        /// </summary>
        [NotNull]
        public IHerculesSink HerculesSink { get; }

        /// <summary>
        /// Name of the Hercules stream to use for emit <see cref="ServiceDiscoveryEvent"/>s.
        /// </summary>
        [NotNull]
        public string StreamName { get; set; } = "serviceDiscovery_event";
    }
}