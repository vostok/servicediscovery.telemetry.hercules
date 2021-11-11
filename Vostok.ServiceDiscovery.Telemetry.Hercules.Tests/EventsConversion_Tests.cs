using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.Tests
{
    [TestFixture]
    internal class EventsConversion_Tests
    {
        private static readonly DateTimeOffset Timestamp = DateTimeOffset.UtcNow;

        private readonly ServiceDiscoveryEvent serviceDiscoveryEvent = new ServiceDiscoveryEvent(
            "vostok",
            "https://github.com/vostok",
            "default",
            ServiceDiscoveryEventKind.ReplicaStart,
            Timestamp,
            new Dictionary<string, string>() {{ServiceDiscoveryEventKeys.EventDescription, "test"}});

        [Test]
        public void Should_correctly_convert_to_hercules_event()
        {
            var herculesTagKeys = typeof(HerculesServiceDiscoveryEventKeys).GetFields().Select(info => (string)info.GetValue(null)).ToArray();

            var herculesEvent = HerculesServiceDiscoveryEventFactory.To(serviceDiscoveryEvent);

            herculesEvent.Timestamp.Should().Be(Timestamp);
            herculesEvent.Tags.Keys.Should().BeEquivalentTo(herculesTagKeys);
            herculesEvent.Tags.Values.Should().NotContainNulls();
        }
        
        [Test]
        public void Should_correctly_convert_from_hercules_event()
        {
            var herculesEvent = HerculesServiceDiscoveryEventFactory.To(serviceDiscoveryEvent);
            var parsedEvent = HerculesServiceDiscoveryEventFactory.From(herculesEvent);

            parsedEvent.Should().BeEquivalentTo(serviceDiscoveryEvent);
        }
    }
}