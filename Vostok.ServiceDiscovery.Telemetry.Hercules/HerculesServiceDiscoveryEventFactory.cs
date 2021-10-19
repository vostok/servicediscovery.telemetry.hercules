using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules
{
    [PublicAPI]
    public static class HerculesServiceDiscoveryEventFactory
    {
        [NotNull]
        public static ServiceDiscoveryEvent From([NotNull] HerculesEvent herculesEvent) =>
            throw new NotImplementedException();
        
        [NotNull]
        public static HerculesEvent To([NotNull] ServiceDiscoveryEvent serviceDiscoveryEvent) =>
            throw new NotImplementedException();
    }
}