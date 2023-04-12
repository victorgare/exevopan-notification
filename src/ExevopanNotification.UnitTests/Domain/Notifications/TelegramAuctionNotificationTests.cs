using ExevopanNotification.Domain.Entities;
using ExevopanNotification.Domain.Enums;
using ExevopanNotification.Domain.Notifications;
using ExevopanNotification.Utils.Utils;
using FluentAssertions;

namespace ExevopanNotification.UnitTests.Domain.Notifications
{
    public class TelegramAuctionNotificationTests
    {
        [Fact]
        public void TelegramAuctionNotification_Should_Return_Notification()
        {
            // arrange
            var auction = new Auction
            {
                Id = 1,
                Nickname = "Nickname1",
                Level = 350,
                CurrentBid = 10000,
                VocationId = VocationEnum.Druid,
                ServerData = new ServerData
                {
                    ServerName = "Server1"
                },
                AuctionEnd = DateTime.Now.AddHours(1).ToUnixTimeSeconds()
            };

            var trendPrice = 1000;

            // act
            var telegramNotification = new TelegramAuctionNotification(auction, trendPrice);

            // assert
            var inlineButton = telegramNotification.GetInlineLinkButton();
            inlineButton.Should().NotBeNull();

            var notificationMessage = telegramNotification.ToString();
            notificationMessage.Should().NotBeNullOrWhiteSpace();
            notificationMessage.Should().Contain(auction.Nickname);
            notificationMessage.Should().Contain(auction.VocationId.ToString());
            notificationMessage.Should().Contain(auction.Level.ToString());
            notificationMessage.Should().Contain(auction.ServerData.ServerName);
            notificationMessage.Should().Contain(auction.AuctionEndDateTime.ToString());

            // formated expected bid number
            notificationMessage.Should().Contain("10.000");

            // formated expected trand value
            notificationMessage.Should().Contain("1.000");

        }
    }
}
