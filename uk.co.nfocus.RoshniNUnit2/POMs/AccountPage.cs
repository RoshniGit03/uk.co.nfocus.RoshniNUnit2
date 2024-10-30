using OpenQA.Selenium;

namespace uk.co.nfocus.RoshniNUnit2.POMs
{
    //POM for the account page
    public class AccountPage
    {
        private readonly IWebDriver _driver;

        public AccountPage(IWebDriver driver) => _driver = driver;

        //method to log out of the account
        public void Logout()
        {
            _driver.FindElement(By.PartialLinkText("account")).Click();
            _driver.FindElement(By.LinkText("Log out")).Click(); 
        }

        //method to navigate to the orders section
        public void NavigateToOrders()
        {
            _driver.FindElement(By.PartialLinkText("account")).Click();
            _driver.FindElement(By.LinkText("Orders")).Click();
        }

        //method to check if an order is present by order number
        public bool IsOrderPresent(string orderNumber)
        {
            try
            {
                var orderElement = _driver.FindElement(By.XPath($"//*[contains(text(), '{orderNumber}')]")); //search for the order number
                return orderElement.Displayed; //return true if the order is displayed
            }
            catch (NoSuchElementException)
            {
                return false; //return false if the order is not found
            }
        }
    }
}
