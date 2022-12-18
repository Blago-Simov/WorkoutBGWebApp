using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace WorkoutBGWebApp
{


    public class Workout_Bg_Tests

    {
        //Arrange
        IWebDriver driver;
        const string pageUrl = "https://workout.bg/";
        const string expectedResult = "ALLNUTRITION SOY PROTEIN";
        const string userAccountTitle = "МОЯТ ПРОФИЛ";
        const string userEmail = "tdxalnmwepnjdmuxtd@tmmwj.com";
        const string userpassword = "£$%*Tuesday";
        IWebElement mainBarSearchForProteinElement;
        IWebElement SearchForVeganProteinElement;
        IWebElement searchForSoyProteinElement;
        IWebElement itemSoyProtein;
        IWebElement itemSoyProteinTitle;
        IWebElement loginButton;
        IWebElement emailTextField;
        IWebElement passwordTextField;
        IWebElement acceptButton;
        IWebElement userProfileLink;
        IWebElement userAccountHeader;
        
        [SetUp]
        public void Setup()
        {
            //Arrange
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl(pageUrl);
            driver.Manage().Window.Maximize();

            
        }

        [Test]
        public void SuccessfulSearchForSoyProtein()
        {
            //Act
             mainBarSearchForProteinElement = 
             driver.FindElement(By.CssSelector(".main-menu-item-1 > .dropdown-toggle > .links-text"));
             mainBarSearchForProteinElement.Click();

             SearchForVeganProteinElement =
             driver.FindElement(By.CssSelector(".menu-item-c221 > .dropdown-toggle > .links-text"));

            Actions action = new Actions(driver);
            action.MoveToElement(SearchForVeganProteinElement).Perform();

            searchForSoyProteinElement = driver.FindElement(By.LinkText("Соев протеин"));
            searchForSoyProteinElement.Click();

            itemSoyProtein = driver.FindElement(By.LinkText("AllNutrition Soy Protein"));
            itemSoyProtein.Click();

            itemSoyProteinTitle = driver.FindElement
            (By.CssSelector("#content > div.product-info > div.product-right > h1"));

            //Assert

            Assert.AreEqual(expectedResult, itemSoyProteinTitle.Text);
        }

        [Test]

        public void successfulLogin()
        {
            
            loginButton = driver.FindElement(By.XPath("//div[3]/div[1]/div/ul/li[1]"));
            loginButton.Click();

            driver.SwitchTo().Frame(0);

            emailTextField = driver.FindElement(By.Id("input-email"));
            passwordTextField = driver.FindElement(By.Id("input-password"));

            emailTextField.SendKeys(userEmail);
            passwordTextField.SendKeys(userpassword);

            acceptButton = driver.FindElement(By.CssSelector(".btn"));
            acceptButton.Click();

            Thread.Sleep(3000);
            userProfileLink = driver.FindElement(By.CssSelector(".top-menu-item > .dropdown-toggle > .links-text"));
            userProfileLink.Click();

            userAccountHeader = driver.FindElement(By.XPath("//body/div[4]/h1"));

            Assert.AreEqual(userAccountTitle, userAccountHeader.Text);
        }

        [TearDown]
        public void TearDown()
        {
           driver.Quit();
        }
    }
}