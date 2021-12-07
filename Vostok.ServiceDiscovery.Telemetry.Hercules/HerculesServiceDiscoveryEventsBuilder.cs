using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules
{
    internal static class HerculesServiceDiscoveryEventsBuilder
    {
        public static void Build([NotNull] ServiceDiscoveryEvent serviceDiscoveryEvent, [NotNull] IHerculesEventBuilder herculesEventBuilder)
        {
            herculesEventBuilder.SetTimestamp(serviceDiscoveryEvent.Timestamp);

            herculesEventBuilder.AddValue(TagNames.Application, serviceDiscoveryEvent.Application);
            herculesEventBuilder.AddValue(TagNames.Environment, serviceDiscoveryEvent.Environment);
            herculesEventBuilder.AddValue(TagNames.Replica, serviceDiscoveryEvent.Replica);
            herculesEventBuilder.AddValue(TagNames.ServiceDiscoveryEventKind, serviceDiscoveryEvent.Kind.ToString());

            herculesEventBuilder.AddContainer(TagNames.Properties,
                builder =>
                {
                    foreach (var property in serviceDiscoveryEvent.Properties)
                        builder.AddValue(property.Key, property.Value);
                });
        }
    }
}