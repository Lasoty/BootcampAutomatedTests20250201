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
}
