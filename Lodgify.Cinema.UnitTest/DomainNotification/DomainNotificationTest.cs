using FluentAssertions;
using Lodgify.Cinema.DomainService.Notification;
using System;
using System.Linq;
using Xunit;

namespace Lodgify.Cinema.UnitTest
{
    public class DomainNotificationTest
    {
        [Trait("Domain.Core", "Notifications")]
        [Fact]
        public void DomainNotification_HasNotifications_True()
        {
            //Arrange
            string notification = "notification";
            DomainNotification domainNotification = new DomainNotification();

            //Act
            domainNotification.Add(notification);

            //Assert
            domainNotification.HasNotification.Should().BeTrue();
            domainNotification.GetNotifications.Contains(notification);
        }

        [Trait("Domain.Core", "Notifications")]
        [Fact]
        public void DomainNotification_AddRange_Success()
        {
            //Arrange
            var notifications = new string[4] { "1", "2", "3", "4" };
            DomainNotification domainNotification = new DomainNotification();

            //Act
            domainNotification.AddRange(notifications);

            //Assert
            domainNotification.HasNotification.Should().BeTrue();
            domainNotification.GetNotifications.Count().Should().Be(notifications.Length);
        }

        [Trait("Domain.Core", "Notifications")]
        [Fact]
        public void DomainNotification_SetUnique_Success()
        {
            //Arrange
            string notification = "notification";
            string uniqueNotification = "uniqueNotification";
            DomainNotification domainNotification = new DomainNotification();

            //Act
            domainNotification.Add(notification);
            domainNotification.SetUniqueNotification(uniqueNotification);

            //Assert
            domainNotification.HasNotification.Should().BeTrue();
            domainNotification.GetNotifications.Single(s => s == uniqueNotification);
        }

        [Trait("Domain.Core", "Notifications")]
        [Fact]
        public void DomainNotification_HasNotifications_False()
        {
            //Arrange
            DomainNotification domainNotification = new DomainNotification();

            //Assert
            domainNotification.HasNotification.Should().BeFalse();
        }
    }
}
