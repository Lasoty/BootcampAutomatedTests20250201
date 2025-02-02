using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ComarchCwiczeniaTesty.E2e.Tests;

public class Tests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }

    [Test]
    public void CheckPageTitle()
    {
        driver.Navigate().GoToUrl("https://example.com");
        driver.Title.Should().Be("Example Domain");
    }

    [Test]
    public void CorrectLoginTest()
    {
        // Arrange
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        IWebElement userNameField = driver.FindElement(By.Id("username"));
        IWebElement passwordField = driver.FindElement(By.Id("password"));

        userNameField.SendKeys("tomsmith");
        passwordField.SendKeys("SuperSecretPassword!");

        var buttonLogin = driver.FindElement(By.XPath("//*[@id=\"login\"]/button"));

        // Act
        buttonLogin.Click();

        // Assert
        var successMessage = driver.FindElement(By.Id("flash"));
        successMessage.Text.Should().Contain("You logged into a secure area!");
    }
}