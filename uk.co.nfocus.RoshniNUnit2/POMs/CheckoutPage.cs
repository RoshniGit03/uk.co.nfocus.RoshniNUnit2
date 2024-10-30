using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace uk.co.nfocus.RoshniNUnit2.POMs
{
    //POM class for Checkout Page interactions
    public class CheckoutPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        //method to fill in billing details required for checkout
        public void CompleteBillingDetails(string firstName, string lastName, string address, string city, string postcode, string phoneNumber)
        {
            _driver.FindElement(By.Id("billing_first_name")).Clear(); 
            _driver.FindElement(By.Id("billing_first_name")).SendKeys(firstName); 

            _driver.FindElement(By.Id("billing_last_name")).Clear(); 
            _driver.FindElement(By.Id("billing_last_name")).SendKeys(lastName); 

            _driver.FindElement(By.Id("billing_address_1")).Clear();
            _driver.FindElement(By.Id("billing_address_1")).SendKeys(address); 

            _driver.FindElement(By.Id("billing_city")).Clear();
            _driver.FindElement(By.Id("billing_city")).SendKeys(city); 

            _driver.FindElement(By.Id("billing_postcode")).Clear(); 
            _driver.FindElement(By.Id("billing_postcode")).SendKeys(postcode);

            _driver.FindElement(By.Id("billing_phone")).Clear();
            _driver.FindElement(By.Id("billing_phone")).SendKeys(phoneNumber);

        }

        //method to select payment method
        public void SelectPaymentMethod(string paymentMethod)
        {
            var paymentOption = _driver.FindElement(By.XPath($"//label[contains(text(), '{paymentMethod}')]"));
            paymentOption.Click();
        }

        //method to place an order and return the order number
        public string PlaceOrder()
        {
            _driver.FindElement(By.Id("place_order")).Click();
            var orderNumberElement = _wait.Until(drv => drv.FindElement(By.CssSelector("#post-6 .woocommerce-order-overview__order > strong")));
            return orderNumberElement.Text;
        }
    }
}
