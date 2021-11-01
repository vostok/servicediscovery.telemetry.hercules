using System;
using System.Linq;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.ServiceDiscovery.Telemetry.Event;
using Tags = Vostok.ServiceDiscovery.Telemetry.Hercules.HerculesServiceDiscoveryEventKeys;

// ReSharper disable AssignNullToNotNullAttribute

namespace Vostok.ServiceDiscovery.Telemetry.Hercules
{
    [PublicAPI]
    public static class HerculesServiceDiscoveryEventFactory
    {
        [NotNull]
        public static ServiceDiscoveryEvent From([NotNull] HerculesEvent herculesEvent)
        {
            var eventKind = Enum.TryParse<ServiceDiscoveryEventKind>(herculesEvent.Tags[Tags.ServiceDiscoveryEventKind]?.AsString, out var kind)
                ? kind
                : throw new ArgumentException(Tags.ServiceDiscoveryEventKind);

            var properties = herculesEvent.Tags[Tags.Properties]
                ?.AsContainer
                .ToDictionary(tag => tag.Key,
                    tag => tag.Value.AsString);

            return new ServiceDiscoveryEvent(
                herculesEvent.Tags[Tags.Application]?.AsString,
                herculesEvent.Tags[Tags.Replica]?.AsString,
                herculesEvent.Tags[Tags.Environment]?.AsString,
                eventKind,
                herculesEvent.Timestamp,
                properties);
        }

        [NotNull]
        public static HerculesEvent To([NotNull] ServiceDiscoveryEvent serviceDiscoveryEvent)
        {
            var builder = new HerculesEventBuilder();
            HerculesServiceDiscoveryEventBuilder.Build(serviceDiscoveryEvent, builder);
            return builder.BuildEvent();
        }
    }
}