using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Hercules.Client.Abstractions.Models;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.EventReader
{
    /// <summary>
    /// Represents configuration of <see cref="HerculesServiceDiscoveryEventsReader"/>.
    /// </summary>
    [PublicAPI]
    public class HerculesServiceDiscoveryEventReadersSettings
    {
        public HerculesServiceDiscoveryEventReadersSettings([NotNull] IHerculesStreamClient herculesStreamClient) =>
            HerculesStreamClient = herculesStreamClient ?? throw new ArgumentNullException(nameof(herculesStreamClient));

        /// <summary>
        /// <see cref="IHerculesStreamClient"/> used to read events.
        /// </summary>
        [NotNull]
        public IHerculesStreamClient HerculesStreamClient { get; }

        /// <summary>
        /// Name of the Hercules stream to use for read <see cref="ServiceDiscoveryEvent"/>s.
        /// </summary>
        [NotNull]
        public string StreamName { get; set; } = "serviceDiscovery_event";

        /// <summary>
        /// Coordinates where reading begins.
        /// </summary>
        [NotNull]
        public StreamCoordinates Coordinates { get; set; } = StreamCoordinates.Empty;
    }
}