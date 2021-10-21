using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Models;
using Vostok.Hercules.Client.Abstractions.Queries;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.EventReader
{
    [PublicAPI]
    public class HerculesServiceDiscoveryEventReader
    {
        private readonly HerculesServiceDiscoveryEventReaderSettings settings;
        private StreamCoordinates coordinates = StreamCoordinates.Empty;

        public HerculesServiceDiscoveryEventReader([NotNull] HerculesServiceDiscoveryEventReaderSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<IEnumerable<ServiceDiscoveryEvent>> ReadAsync(TimeSpan timeout, CancellationToken cancellationToken = new CancellationToken())
        {
            var query = new ReadStreamQuery(settings.StreamName) {Coordinates = coordinates};
            var readResult = await settings.HerculesStreamClient.ReadAsync(query, timeout, cancellationToken);
            readResult.EnsureSuccess();

            coordinates = readResult.Payload.Next;
            return readResult.Payload.Events.Select(herculesEvent => HerculesServiceDiscoveryEventFactory.From(herculesEvent)).ToList();
        }
    }
}