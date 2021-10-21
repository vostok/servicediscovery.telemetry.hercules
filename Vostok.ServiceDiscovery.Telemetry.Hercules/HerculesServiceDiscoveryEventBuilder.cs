using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.ServiceDiscovery.Telemetry.Event;
using Tags = Vostok.ServiceDiscovery.Telemetry.Hercules.HerculesServiceDiscoveryEventKeys;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules
{
    internal static class HerculesServiceDiscoveryEventBuilder
    {
        public static void Build([NotNull] ServiceDiscoveryEvent serviceDiscoveryEvent, [NotNull] IHerculesEventBuilder herculesEventBuilder)
        {
            herculesEventBuilder.SetTimestamp(serviceDiscoveryEvent.Timestamp);

            herculesEventBuilder.AddValue(Tags.Application, serviceDiscoveryEvent.Application);
            herculesEventBuilder.AddValue(Tags.Environment, serviceDiscoveryEvent.Environment);
            herculesEventBuilder.AddValue(Tags.Replica, serviceDiscoveryEvent.Replica);
            herculesEventBuilder.AddValue(Tags.ServiceDiscoveryEventKind, serviceDiscoveryEvent.ServiceDiscoveryEventKind.ToString());

            herculesEventBuilder.AddContainer(Tags.Properties,
                builder =>
                {
                    foreach (var property in serviceDiscoveryEvent.Properties)
                        builder.AddValue(property.Key, property.Value);
                });
        }
    }
}