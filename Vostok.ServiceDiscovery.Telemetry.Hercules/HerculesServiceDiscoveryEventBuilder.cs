using System;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules
{
    internal static class HerculesServiceDiscoveryEventBuilder
    {
        public static void Build([NotNull] ServiceDiscoveryEvent serviceDiscoveryEvent, [NotNull] IHerculesEventBuilder herculesEventBuilder) =>
            throw new NotImplementedException();
    }
}