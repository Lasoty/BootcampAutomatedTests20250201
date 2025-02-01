using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComachCwiczeniaTesty.UnitTests;

[TestFixture]
public class CalculatorTests
{
    private Calculator calculator;

    [OneTimeSetUp]
    public void SetUpFixture()
    {
        // Tworzymy instancję całej klasy testów
    }

    [SetUp]
    public void SetUp()
    {
        // Tworzymy ustawienia per każdy test
        calculator = new Calculator();
    }

    [Test]
    public void Add_WhenCalled_ReturnsSumOfArguments()
    {
        // Act
        var result = calculator.Add(1, 2);
        // Assert
        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void Add_WhenCalled_ReturnsSumOfArguments2()
    {
        // Act
        var result = calculator.Add(1, 2);
        // Assert
        Assert.That(result, Is.EqualTo(3));
    }


    [TearDown]
    public void TearDown()
    {
        // Sprzątanie po każdym teście
    }

    [OneTimeTearDown]
    public void TearDownFixture()
    {
        // Sprzątanie po wszystkich testach
    }
}
