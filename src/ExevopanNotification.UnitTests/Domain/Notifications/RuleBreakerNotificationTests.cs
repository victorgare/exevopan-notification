using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Notifications;
using FluentAssertions;

namespace ExevopanNotification.UnitTests.Domain.Notifications
{
    public class RuleBreakerNotificationTests
    {
        [Fact]
        public void ShouldCalculatePricePerMillionOfXpCorrectly()
        {
            // arrange
            var auction = new Auction
            {
                CurrentBid = 70251,
                Level = 1020
            };

            var notication = new RuleBreakerNotification(auction);

            // act
            var result = notication.ToString();

            // assert
            result.Should().Contain("💰(4tc/kk)");
        }
    }
}
