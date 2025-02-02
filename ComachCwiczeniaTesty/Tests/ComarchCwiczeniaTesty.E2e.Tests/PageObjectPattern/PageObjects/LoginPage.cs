using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace ComarchCwiczeniaTesty.E2e.Tests.PageObjectPattern.PageObjects;

public class LoginPage
{
    private readonly IWebDriver driver;

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    private IWebElement UserNameField => driver.FindElement(By.Id("username"));
    private IWebElement PasswordField => driver.FindElement(By.Id("password"));
    private IWebElement ButtonLogin => driver.FindElement(By.XPath("//*[@id=\"login\"]/button"));
    private IWebElement ErrorMessage => driver.FindElement(By.Id("flash"));

    public void Open()
    {
        driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
    }

    public void Login(string userName, string password)
    {
        UserNameField.SendKeys(userName);
        PasswordField.SendKeys(password);
        ButtonLogin.Click();
    }

    public bool IsErrorMessageDisplayed() => ErrorMessage.Displayed && ErrorMessage.Text.Contains("is invalid!");
}
