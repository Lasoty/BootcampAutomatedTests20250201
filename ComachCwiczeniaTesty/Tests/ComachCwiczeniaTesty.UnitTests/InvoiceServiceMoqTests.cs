using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

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
        taxServiceMock.Setup(ts => ts.GetTax(It.IsAny<decimal>())).Verifiable();

        // Act
        await cut.CalculateTotal(amount, customerType);

        // Assert
        taxServiceMock.Verify(ts => ts.GetTax(It.IsAny<decimal>()), Times.Once);
    }
}
