using System;
using System.Linq;
using JetBrains.Annotations;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules
{
    /// <summary>
    /// Converts <see cref="HerculesEvent"/>s written by <see cref="HerculesServiceDiscoveryEventsSender"/> back to <see cref="ServiceDiscoveryEvent"/>s.
    /// </summary>
    [PublicAPI]
    public static class HerculesServiceDiscoveryEventsFactory
    {
        [NotNull]
        public static ServiceDiscoveryEvent CreateFrom([NotNull] HerculesEvent herculesEvent)
        {
            if (!Enum.TryParse<ServiceDiscoveryEventKind>(herculesEvent.Tags[TagNames.Kind]?.AsString, out var kind))
                throw new ArgumentException(nameof(TagNames.Kind));
            var properties = herculesEvent.Tags[TagNames.Properties]
                ?.AsContainer
                .ToDictionary(tag => tag.Key, tag => tag.Value.AsString);

            return new ServiceDiscoveryEvent(
                kind,
                herculesEvent.Tags[TagNames.Environment]?.AsString ?? throw new ArgumentException(nameof(TagNames.Environment)),
                herculesEvent.Tags[TagNames.Application]?.AsString ?? throw new ArgumentException(nameof(TagNames.Application)),
                herculesEvent.Tags[TagNames.Replica]?.AsString ?? throw new ArgumentException(nameof(TagNames.Replica)),
                herculesEvent.Timestamp,
                properties ?? throw new ArgumentException(nameof(TagNames.Properties)));
        }
    }
}