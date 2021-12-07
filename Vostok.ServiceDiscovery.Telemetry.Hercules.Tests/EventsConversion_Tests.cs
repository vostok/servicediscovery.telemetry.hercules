using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Vostok.Hercules.Client.Abstractions.Events;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.Tests
{
    [TestFixture]
    internal class EventsConversion_Tests
    {
        private static readonly DateTimeOffset Timestamp = DateTimeOffset.UtcNow;

        private readonly ServiceDiscoveryEvent serviceDiscoveryEvent = new ServiceDiscoveryEvent(
            ServiceDiscoveryEventKind.ReplicaStarted,
            "default",
            "vostok",
            "https://github.com/vostok",
            Timestamp,
            new Dictionary<string, string> {{ServiceDiscoveryEventWellKnownProperties.Description, "test"}});
        private HerculesEventBuilder builder;

        [SetUp]
        public void SetUp() =>
            builder = new HerculesEventBuilder();

        [Test]
        public void Should_correctly_convert_to_hercules_event()
        {
            var herculesTagKeys = typeof(TagNames).GetFields().Select(info => (string)info.GetValue(null)).ToArray();

            HerculesServiceDiscoveryEventsBuilder.Build(serviceDiscoveryEvent, builder);
            var herculesEvent = builder.BuildEvent();

            herculesEvent.Timestamp.Should().Be(Timestamp);
            herculesEvent.Tags.Keys.Should().BeEquivalentTo(herculesTagKeys);
            herculesEvent.Tags.Values.Should().NotContainNulls();
        }

        [Test]
        public void Should_correctly_convert_from_hercules_event()
        {
            HerculesServiceDiscoveryEventsBuilder.Build(serviceDiscoveryEvent, builder);
            var herculesEvent = builder.BuildEvent();
            var parsedEvent = HerculesServiceDiscoveryEventsFactory.From(herculesEvent);

            parsedEvent.Should().BeEquivalentTo(serviceDiscoveryEvent);
        }
    }
}