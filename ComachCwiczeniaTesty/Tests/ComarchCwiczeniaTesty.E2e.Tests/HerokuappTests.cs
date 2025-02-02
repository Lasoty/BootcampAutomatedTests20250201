using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ComarchCwiczeniaTesty.E2e.Tests;

public class HerokuappTests
{
    private IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        ChromeOptions options = new();
        options.AddArgument("headless");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--window-size=1920,1080");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--ignore-certificate-errors");
        options.AddArgument("--allow-insecure-localhost");
        options.AddArgument("--lang=en-US");

        new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.ChromeConfig());
        driver = new ChromeDriver(options);
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

    [Test]
    public void IncorrectLoginTest()
    {
        // Arrange
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");

        IWebElement userNameField = driver.FindElement(By.Id("username"));
        IWebElement passwordField = driver.FindElement(By.Id("password"));

        userNameField.SendKeys("wrongUser");
        passwordField.SendKeys("wrongPassword");

        var buttonLogin = driver.FindElement(By.XPath("//*[@id=\"login\"]/button"));

        // Act
        buttonLogin.Click();

        // Assert
        var errorMessage = driver.FindElement(By.Id("flash"));
        errorMessage.Text.Should().Contain("Your username is invalid!");
    }

    [Test]
    public void CheckboxIsCheckedTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/checkboxes");
        var checkbox = driver.FindElement(By.XPath("//*[@id=\"checkboxes\"]/input[2]"));
        checkbox.Selected.Should().BeTrue();
    }

    [Test]
    public void DropdownTest()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dropdown");
        var dropdown = driver.FindElement(By.Id("dropdown"));
        var selectElement = new SelectElement(dropdown);
        selectElement.SelectByValue("1");

        selectElement.SelectedOption.Text.Should().Be("Option 1");

        selectElement.SelectByValue("2");
        selectElement.SelectedOption.Text.Should().Be("Option 2");
    }

    [Test]
    public void HandleJavaScriptAlerts()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
        var alertButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[1]/button"));
        alertButton.Click();

        var alert = driver.SwitchTo().Alert();

        alert.Text.Should().Be("I am a JS Alert");
        alert.Accept();

        var resultText = driver.FindElement(By.Id("result"));
        resultText.Text.Should().Contain("You successfully clicked an alert");

    }

    [Test]
    public void HandleJavaScriptConfirmAlerts()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
        var alertButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[2]/button"));
        alertButton.Click();

        var alert = driver.SwitchTo().Alert();

        alert.Text.Should().Be("I am a JS Confirm");
        alert.Accept();

        var resultText = driver.FindElement(By.Id("result"));
        resultText.Text.Should().Contain("You clicked: Ok");
    }

    [Test]
    public void HandleJavaScriptDismissAlerts()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
        var alertButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[2]/button"));
        alertButton.Click();

        var alert = driver.SwitchTo().Alert();

        alert.Text.Should().Be("I am a JS Confirm");
        alert.Dismiss();

        var resultText = driver.FindElement(By.Id("result"));
        resultText.Text.Should().Contain("You clicked: Cancel");
    }

    [Test]
    public void HandleJavaScriptInputAlerts()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
        var alertButton = driver.FindElement(By.XPath("//*[@id=\"content\"]/div/ul/li[3]/button"));
        alertButton.Click();

        var alert = driver.SwitchTo().Alert();

        alert.Text.Should().Be("I am a JS prompt");
        alert.SendKeys("Selenium test");
        alert.Accept();

        var resultText = driver.FindElement(By.Id("result"));
        resultText.Text.Should().Contain("You entered: Selenium test");
    }

    [Test]
    public void TestDynamicLoading()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");
        var startButton = driver.FindElement(By.XPath("//*[@id=\"start\"]/button"));
        startButton.Click();

        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        var loadedElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("finish")));
        loadedElement.Text.Should().Be("Hello World!");
    }
}