using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.EventReader
{
    [PublicAPI]
    public class HerculesServiceDiscoveryEventReader
    {
        private readonly HerculesServiceDiscoveryEventReaderSettings settings;

        public HerculesServiceDiscoveryEventReader([NotNull] HerculesServiceDiscoveryEventReaderSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));;
        }

        public async Task<ServiceDiscoveryEvent> ReadAsync(TimeSpan timeout, CancellationToken cancellationToken = new CancellationToken()) =>
            throw new NotImplementedException();
    }
}