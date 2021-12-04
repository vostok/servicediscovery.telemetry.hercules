using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Models;
using Vostok.Hercules.Client.Abstractions.Queries;
using Vostok.Logging.Abstractions;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.EventReader
{
    [PublicAPI]
    public class HerculesServiceDiscoveryEventReader
    {
        private StreamCoordinates coordinates;
        private readonly HerculesServiceDiscoveryEventReaderSettings settings;
        private readonly ILog log;

        public HerculesServiceDiscoveryEventReader([NotNull] HerculesServiceDiscoveryEventReaderSettings settings, ILog log)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.log = (log ?? LogProvider.Get()).ForContext<HerculesServiceDiscoveryEventReader>();
            coordinates = this.settings.Coordinates;
        }

        [ItemCanBeNull]
        public async Task<IList<ServiceDiscoveryEvent>> ReadAsync(TimeSpan timeout, CancellationToken cancellationToken = new CancellationToken())
        {
            var query = new ReadStreamQuery(settings.StreamName) {Coordinates = coordinates};
            var readResult = await settings.HerculesStreamClient.ReadAsync(query, timeout, cancellationToken).ConfigureAwait(false);
            if (!readResult.IsSuccessful)
            {
                log.Info(
                    "Reading failed with status: {Status} and error details: {ErrorDetails}.",
                    readResult.Status,
                    readResult.ErrorDetails);
                return null;
            }

            coordinates = readResult.Payload.Next;
            return readResult.Payload.Events.Select(herculesEvent => HerculesServiceDiscoveryEventFactory.From(herculesEvent)).ToList();
        }
    }
}