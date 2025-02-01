using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace ComachCwiczeniaTesty.UnitTests;

public class InvoiceServiceTests
{
    private InvoiceService cut;

    [SetUp]
    public void SetUp()
    {
        cut = new InvoiceService();
    }

    [Test]
    public void GenerateInvoiceNumber_ShouldStartWith_INV()
    {
        // Arrange
        string expected = "INV";
        // Act
        string actual = cut.GenerateInvoiceNumber();

        // Assert
        actual.Should().StartWith(expected);
    }

    [Test]
    public void GenerateInvoiceNumber_ShouldContainsNumericSections()
    {
        //Act
        string actual = cut.GenerateInvoiceNumber();

        //Assert
        actual.Should().MatchRegex(@"INV-\d{8}-\d{3}");
        actual.Should().Match("INV-????????-???");
    }
}
