using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Range = Moq.Range;

namespace ComachCwiczeniaTesty.UnitTests;

public class InvoiceServiceMoqTests
{
    private Mock<ITaxService> taxServiceMock;
    private Mock<IDiscountService> discountServiceMock;
    private InvoiceService cut;

    [SetUp]
    public void Setup()
    {
        taxServiceMock = new Mock<ITaxService>();
        discountServiceMock = new Mock<IDiscountService>();

        cut = new InvoiceService(taxServiceMock.Object, discountServiceMock.Object);
    }

    [Test]
    public async Task CalculateTotal_WhenCalled_VerifiesTaxServiceGetTaxIsCalled()
    {
        // Arrange
        decimal amount = 100;
        string customerType = "Regular";
        discountServiceMock.Setup(ds => ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>())).Returns(10m);
        taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>())).Returns(5).Verifiable();

        // Act
        await cut.CalculateTotal(amount, customerType);

        // Assert
        taxServiceMock.Verify(ts => ts.GetTax(It.IsAny<decimal>()), Times.Once);
    }

    [TestCase(950, "Regular", 935)]
    [TestCase(100, "Regular", 95)]
    [TestCase(1000, "Regular", 985)]
    [TestCase(100, "VIP", 55)]
    public async Task CalculateTotal_WhenCalled_ReturnsCorrectTotal(decimal amount, string customerType, decimal expected)
    {
        // Arrange
        discountServiceMock.Setup(ds => ds.CalculateDiscount(It.IsInRange<decimal>(0,999, Range.Inclusive), It.IsAny<string>())).Returns(10m);
        discountServiceMock.Setup(ds => ds.CalculateDiscount(It.IsInRange<decimal>(900, int.MaxValue, Range.Inclusive), It.IsAny<string>()))
            .Returns(20m);
        discountServiceMock.Setup(ds => 
            ds.CalculateDiscount(
                It.IsAny<decimal>(), 
                It.Is<string>(v => v.Equals("VIP", StringComparison.InvariantCultureIgnoreCase))))
            .Returns(50m);
        taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>())).Returns(5m);
        
        // Act
        var result = await cut.CalculateTotal(amount, customerType);
        
        // Assert
        result.Should().Be(expected);
    }

    [Test]
    public async Task CalculateTotal_ThrowsWhenTaxServiceCalled()
    {
        // Arrange
        decimal amount = 100;
        string customerType = "Regular";
        discountServiceMock.Setup(ds => ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>())).Returns(10m);
        taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>())).Throws(new Exception("Tax calculation failed"));

        // Act & Assert

        Func<Task> call = async () =>  await cut.CalculateTotal(amount, customerType);
        await call.Should().ThrowAsync<Exception>().WithMessage("Tax calculation failed");
    }

    [Test]
    public async Task CalculateTotal_CallsServicesInSequence()
    {
        // Arrange
        var sequence = new MockSequence();
        taxServiceMock = new Mock<ITaxService>(MockBehavior.Strict);
        discountServiceMock = new Mock<IDiscountService>(MockBehavior.Strict);
        cut = new InvoiceService(taxServiceMock.Object, discountServiceMock.Object);

        decimal amount = 100;
        string customerType = "Regular";
        discountServiceMock.InSequence(sequence).Setup(ds => ds.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>())).Returns(10m);
        taxServiceMock.InSequence(sequence).Setup(ts => ts.GetTax(It.IsAny<decimal>())).Returns(5m);

        // Act
        var result = await cut.CalculateTotal(amount, customerType);

        // Assert
        result.Should().Be(95);
    }

    [Test]
    public void PolimorfizmTest()
    {
        Bus bus = new Bus();

        Assert.Pass();
    }
}
