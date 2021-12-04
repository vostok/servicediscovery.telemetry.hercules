using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Hercules.Client.Abstractions.Models;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.EventReader
{
    [PublicAPI]
    public class HerculesServiceDiscoveryEventReaderSettings
    {
        public HerculesServiceDiscoveryEventReaderSettings([NotNull] IHerculesStreamClient herculesStreamClient) =>
            HerculesStreamClient = herculesStreamClient ?? throw new ArgumentNullException(nameof(herculesStreamClient));

        [NotNull]
        public IHerculesStreamClient HerculesStreamClient { get; }

        [NotNull]
        public string StreamName { get; set; } = "serviceDiscovery_event";

        [NotNull]
        public StreamCoordinates Coordinates { get; set; } = StreamCoordinates.Empty;
    }
}