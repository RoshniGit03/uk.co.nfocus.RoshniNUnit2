using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace uk.co.nfocus.RoshniNUnit2
{
    //base test class to set up and tear down the WebDriver
    public class BaseTest
    {
        protected IWebDriver Driver;
        protected string BaseUrl = "https://www.edgewordstraining.co.uk/demo-site"; //base URL for the tests

        [SetUp]
        public void SetUp()
        {
            //initialise ChromeDriver and maximize the window
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl($"{BaseUrl}/my-account/"); //navigate to the account page

            //call method to dismiss demo Store banner if present
            DismissDemoBanner();


        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }

        //method to dismiss banner
        private void DismissDemoBanner()
        {
            try
            {
                //wait for the banner dismiss button to be clickable
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                var bannerDismissButton = wait.Until(driver => driver.FindElement(By.CssSelector("body > p > a")));
                bannerDismissButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Demo banner not found or could not be dismissed.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Demo banner dismiss button does not exist.");
            }
        }
    }
}
