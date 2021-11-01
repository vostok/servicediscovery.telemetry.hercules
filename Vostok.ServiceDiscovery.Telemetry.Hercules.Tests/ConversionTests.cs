using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Vostok.ServiceDiscovery.Telemetry.Event;

namespace Vostok.ServiceDiscovery.Telemetry.Hercules.Tests
{
    [TestFixture]
    internal class ConversionTests
    {
        [Test]
        public void Should_correctly_convert_to_hercules_event()
        {
            var sdEvent = new ServiceDiscoveryEvent("app",
                "replica",
                "env",
                ServiceDiscoveryEventKind.ReplicaStart,
                DateTimeOffset.Now,
                new Dictionary<string, string> {{"prop1", "val1"}, {"prop2", "val2"}});

            var herculesEvent = HerculesServiceDiscoveryEventFactory.To(sdEvent);
            var parsedSdEvent = HerculesServiceDiscoveryEventFactory.From(herculesEvent);

            sdEvent.Should().BeEquivalentTo(parsedSdEvent);
        }
    }
}