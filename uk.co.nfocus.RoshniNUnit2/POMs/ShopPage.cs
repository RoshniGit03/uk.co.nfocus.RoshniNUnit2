using OpenQA.Selenium;

namespace uk.co.nfocus.RoshniNUnit2.POMs
{
    //POM class to interact with the Shop Page elements
    public class ShopPage
    {
        private readonly IWebDriver _driver;


        public ShopPage(IWebDriver driver) => _driver = driver;

        //page elements related to adding items to the cart
        private IWebElement PoloItem => _driver.FindElement(By.PartialLinkText("Polo"));
        private IWebElement AddToCartButton => _driver.FindElement(By.CssSelector("button[name='add-to-cart']"));

        //method to select a specific item and add to cart
        public void AddPoloToCart()
        {
            PoloItem.Click();
            AddToCartButton.Click();
        }
    }
}
