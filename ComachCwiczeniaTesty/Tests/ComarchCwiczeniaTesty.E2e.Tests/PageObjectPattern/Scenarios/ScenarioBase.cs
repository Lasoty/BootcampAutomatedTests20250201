using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ComarchCwiczeniaTesty.E2e.Tests.PageObjectPattern.Scenarios;

public class ScenarioBase
{
    protected IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        ChromeOptions options = new();
        options.AddArgument("headless");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--window-size=1920,1080");

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
}