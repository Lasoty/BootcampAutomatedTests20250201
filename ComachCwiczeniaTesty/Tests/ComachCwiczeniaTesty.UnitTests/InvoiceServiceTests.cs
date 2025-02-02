using FluentAssertions;

namespace ComachCwiczeniaTesty.UnitTests;

public class InvoiceServiceTests
{
    private InvoiceService cut;

    [SetUp]
    public void SetUp()
    {
        cut = new InvoiceService(null, null);
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


    // Testowanie kolekcji

    [Test]
    public void GetInvoiceItems_ShouldContain_ItemWithSpecificName()
    {
        // Act
        List<InvoiceItem> items = cut.GetInvoiceItems();

        // Assert
        items.Should().Contain(i => i.Name == "Product 1");
        items.Should().ContainSingle(i => i.Name == "Product 1");
    }

    [Test]
    public void GetInvoiceItems_AllItems_ShouldHavePositiveQuantity()
    {
        // Act
        List<InvoiceItem> items = cut.GetInvoiceItems();

        // Assert
        items.Should().OnlyContain(i => i.Quantity > 0);
    }

    [Test]
    public void GetInvoiceItems_ShouldHave_ItemsInAscendingOrderByQuantity()
    {
        // Act
        List<InvoiceItem> items = cut.GetInvoiceItems();

        // Assert
        items.Should().BeInAscendingOrder(i => i.Quantity);
    }
}
