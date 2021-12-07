using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions;
using Vostok.Hercules.Client.Abstractions.Models;
using Vostok.Hercules.Client.Abstractions.Queries;
using Vostok.Hercules.Client.Abstractions.Results;
using Vostok.Logging.Abstractions;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.EventReader
{
    /// <summary>
    /// <para><see cref="HerculesServiceDiscoveryEventsReader"/> that allows you to read events
    /// using the <see cref="IHerculesStreamClient"/> transmitted in the <see cref="HerculesServiceDiscoveryEventReadersSettings"/>.</para>
    /// <para>Reading events starts from the <see cref="HerculesServiceDiscoveryEventReadersSettings.Coordinates"/>, subsequent readings automatically move the coordinates.</para>
    /// </summary>
    [PublicAPI]
    public class HerculesServiceDiscoveryEventsReader
    {
        private StreamCoordinates coordinates;
        private readonly HerculesServiceDiscoveryEventReadersSettings settings;
        private readonly ILog log;

        public HerculesServiceDiscoveryEventsReader([NotNull] HerculesServiceDiscoveryEventReadersSettings settings, [CanBeNull] ILog log = null)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.log = (log ?? LogProvider.Get()).ForContext<HerculesServiceDiscoveryEventsReader>();
            coordinates = this.settings.Coordinates;
        }

        /// <summary>
        /// <para>Reads <see cref="ServiceDiscoveryEvent"/>s using the <see cref="HerculesServiceDiscoveryEventReadersSettings.HerculesStreamClient"/>.
        /// See <see cref="IHerculesStreamClient.ReadAsync"/> for details.</para>
        /// <para>Return <b>Null</b> if reading unsuccessful (<see cref="HerculesStatus"/>).</para>
        /// </summary>
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
            return readResult.Payload.Events.Select(herculesEvent => HerculesServiceDiscoveryEventsFactory.From(herculesEvent)).ToList();
        }
    }
}