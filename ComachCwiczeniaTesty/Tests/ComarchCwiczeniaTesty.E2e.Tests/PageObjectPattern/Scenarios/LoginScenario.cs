using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczeniaTesty.E2e.Tests.PageObjectPattern.PageObjects;
using FluentAssertions;

namespace ComarchCwiczeniaTesty.E2e.Tests.PageObjectPattern.Scenarios;

public class LoginScenario : ScenarioBase
{
    [Test]
    public void LoginWithInvalidCredentials()
    {
        LoginPage loginPage = Login();
        loginPage.Login("tomsmith", "badpassword");
        loginPage.IsErrorMessageDisplayed().Should().BeTrue();
    }
    
    [Test]
    public void LoginWithValidCredentials()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Open();
        loginPage.Login("tomsmith", "SuperSecretPassword!");
        loginPage.IsErrorMessageDisplayed().Should().BeFalse();
    }

    private LoginPage Login()
    {
        var loginPage = new LoginPage(driver);
        loginPage.Open();
        return loginPage;
    }
}