using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules
{
    /// <summary>
    /// Represents configuration of <see cref="HerculesServiceDiscoveryEventsSender"/>.
    /// </summary>
    [PublicAPI]
    public class HerculesServiceDiscoveryEventsSenderSettings
    {
        public HerculesServiceDiscoveryEventsSenderSettings([NotNull] IHerculesSink herculesSink, [NotNull] string streamName)
        {
            HerculesSink = herculesSink ?? throw new ArgumentNullException(nameof(herculesSink));
            StreamName = streamName ?? throw new ArgumentNullException(nameof(streamName));
        }

        /// <summary>
        /// <see cref="IHerculesSink"/> used to emit events.
        /// </summary>
        [NotNull]
        public IHerculesSink HerculesSink { get; }

        /// <summary>
        /// Name of the Hercules stream to use for emit <see cref="ServiceDiscoveryEvent"/>s.
        /// </summary>
        [NotNull]
        public string StreamName { get; }
    }
}