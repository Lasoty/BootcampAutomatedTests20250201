using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace ComachCwiczeniaTesty.UnitTests;

public class DateCalculatorTests
{
    private DateCalculator cut;

    [SetUp]
    public void SetUp()
    {
        cut = new DateCalculator();
    }

    [Test]
    public void GetNextBusinessDay_WhenFridayIsGiven_ShouldReturnMonday()
    {
        // Arrange
        DateTime friday = 31.January(2025);
        // Act
        var result = cut.GetNextBusinessDay(friday);
        // Assert
        result.Should().Be(3.February(2025));
    }

    [Test]
    public void GetNextBusinessDay_WhenSaturdayIsGiven_ShouldReturnMonday()
    {
        // Arrange
        DateTime saturday = 1.February(2025);
        // Act
        var result = cut.GetNextBusinessDay(saturday);
        // Assert
        result.Should().Be(3.February(2025));
    }

    [Test]
    public void GetNextBusinessDay_WhenSundayIsGiven_ShouldReturnMonday()
    {
        // Arrange
        DateTime sunday = 2.February(2025);
        // Act
        var result = cut.GetNextBusinessDay(sunday);
        // Assert
        result.Should().Be(3.February(2025));
    }

    [Test]
    public void GetNextBusinessDay_ShouldHandleLeapYear()
    {
        // Test w roku przestępnym, 28 lutego -> Następny dzień roboczy to 1 marca
        cut.GetNextBusinessDay(new DateTime(2024, 2,
            28)).Should().Be(new DateTime(2024, 2, 29));
    }
    [Test]
    public void GetNextBusinessDay_ShouldNotChangeTimePart()
    {
        // Test zachowania części czasowej
        var time = new DateTime(2024, 2, 23, 15, 30, 0); // 15:30
        cut.GetNextBusinessDay(time).TimeOfDay.Should().Be(time.TimeOfDay);
    }
    [Test]
    public void GetNextBusinessDay_ShouldBeAfterInputDate()
    {
        // Test, że zwracana data jest zawsze po podanej dacie
        var date = new DateTime(2024, 2, 23);
        cut.GetNextBusinessDay(date).Should().BeAfter(date);
    }
    [Test]
    public void GetNextBusinessDay_ShouldBeCloseToInputDate()
    {
        // Test, że zwracana data jest blisko podanej daty (nie więcej niż 3 dni później)
        var date = new DateTime(2024, 2, 23);
        cut.GetNextBusinessDay(date).Should().BeCloseTo(date, TimeSpan.FromDays(3));
    }
    [Test]
    public void GetNextBusinessDay_ShouldBeOnWeekday()
    {
        // Test, że zwracana data jest dniem roboczym
        var date = new DateTime(2024, 2, 23);
        var nextBusinessDay = cut.GetNextBusinessDay(date);
        nextBusinessDay.DayOfWeek.Should().NotBe(DayOfWeek.Saturday).And.NotBe(DayOfWeek.Sunday);
    }
}
