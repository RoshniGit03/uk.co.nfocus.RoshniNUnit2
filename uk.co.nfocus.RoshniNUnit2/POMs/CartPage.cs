using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Globalization;

namespace uk.co.nfocus.RoshniNUnit2.POMs
{
    //POM class for the Cart Page with methods for coupon application and total calculations
    public class CartPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        //intialise WebDriver and WebDriverWait
        public CartPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        //elements for coupon and cart total values
        private IWebElement CouponField => _driver.FindElement(By.Id("coupon_code"));
        private IWebElement ApplyCouponButton => _driver.FindElement(By.CssSelector("button[name='apply_coupon']"));
        private IWebElement CouponDiscount => _wait.Until(drv => drv.FindElement(By.CssSelector(".cart-discount.coupon-edgewords td span")));
        private IWebElement Subtotal => _wait.Until(drv => drv.FindElement(By.CssSelector(".cart-collaterals .cart-subtotal td span")));
        private IWebElement Shipping => _wait.Until(drv => drv.FindElement(By.CssSelector("#shipping_method span bdi")));
        private IWebElement Total => _wait.Until(drv => drv.FindElement(By.CssSelector(".order-total td strong span bdi")));

        //method to apply a discount coupon
        public void ApplyCoupon(string couponCode)
        {
            CouponField.SendKeys(couponCode);
            ApplyCouponButton.Click();
        }

        // Retrieves values as a tuple
        public (decimal subtotal, decimal discount, decimal shipping, decimal total) GetCartTotals()
        {
            //parse the currency values and convert to decimal
            decimal subtotal = decimal.Parse(Subtotal.Text.Replace("£", "").Trim(), CultureInfo.InvariantCulture);
            decimal discount = decimal.Parse(CouponDiscount.Text.Replace("£", "").Trim(), CultureInfo.InvariantCulture);
            decimal shipping = decimal.Parse(Shipping.Text.Replace("£", "").Trim(), CultureInfo.InvariantCulture);
            decimal total = decimal.Parse(Total.Text.Replace("£", "").Trim(), CultureInfo.InvariantCulture);
            return (subtotal, discount, shipping, total);
        }

        //method to proceed to checkout page
        public void ProceedToCheckout()
        {
            _driver.FindElement(By.PartialLinkText("checkout")).Click();
        }
    }
}
