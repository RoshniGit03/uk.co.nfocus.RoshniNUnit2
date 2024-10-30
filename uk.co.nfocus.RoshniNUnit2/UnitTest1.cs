using NUnit.Framework;
using OpenQA.Selenium;
using uk.co.nfocus.RoshniNUnit2.POMs; //import POM namespace

namespace uk.co.nfocus.RoshniNUnit2
{
    //DiscountTests class that inherits from BaseTest for test setup/teardown
    public class DiscountTests : BaseTest
    {
        //test method for applying discount coupon and verifying totals
        [Test]
        public void TestDiscountPurchase()
        {
            //initialize page object for login
            var loginPage = new LoginPage(Driver); //create an instance of LoginPage

            //retrieve credentials from environment variables
            string username = Environment.GetEnvironmentVariable("USERNAME");
            string password = Environment.GetEnvironmentVariable("PASSWORD");

            //fallback - use hardcoded credentials if environment variables are not set
            if (string.IsNullOrEmpty(username))
            {
                username = "email@address.com"; //use pre-established account email & pswd
            }

            if (string.IsNullOrEmpty(password))
            {
                password = "strong!password";
            }

            //check if the credentials are set
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username or password cannot be null or empty. Please set the environment variables or use hardcoded values.");
            }

            //login using credentials
            loginPage.Login(username, password);

            //navigate to shop page to select an item to purchase
            var shopPage = new ShopPage(Driver);
            Driver.FindElement(By.LinkText("Shop")).Click();

            //add specific item (polo shirt) to the cart
            shopPage.AddPoloToCart();

            //view the cart to proceed to checkout steps
            var cartPage = new CartPage(Driver);
            Driver.FindElement(By.CssSelector("a[href*='cart']")).Click();

            //apply coupon code for discount and retrieve cart totals
            cartPage.ApplyCoupon("edgewords");
            var (subtotal, discount, shipping, total) = cartPage.GetCartTotals();

            //calculate the expected discount as 15% of subtotal and verify
            decimal expectedDiscount = subtotal * 0.15m;
            Assert.That(discount, Is.EqualTo(expectedDiscount).Within(0.01m), "Coupon discount is not 15% of the subtotal.");
            // Used Within(0.01m) to allow for minor floating-point precision errors in financial calculations

            //calculate expected total after discount and shipping, verify it
            decimal expectedTotal = subtotal - discount + shipping; 
            Assert.That(total, Is.EqualTo(expectedTotal).Within(0.01m), "Total amount after discount and shipping is incorrect.");

            //log success message if both assertions pass
            Console.WriteLine("Test passed: Coupon removes 15% and total amount matches the expected total after discount and shipping.");

            //logout from account to complete test case
            var accountPage = new AccountPage(Driver);
            accountPage.Logout();
        }

        //test method to verify that a placed order appears in the order history
        [Test]
        public void TestOrderNumber()
        {
            //login and checkout, as in Test 1
            var loginPage = new LoginPage(Driver);

            string username = Environment.GetEnvironmentVariable("USERNAME");
            string password = Environment.GetEnvironmentVariable("PASSWORD");

            if (string.IsNullOrEmpty(username))
            {
                username = "email@address.com"; 
            }

            if (string.IsNullOrEmpty(password))
            {
                password = "strong!password";
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username or password cannot be null or empty. Please set the environment variables or use hardcoded values.");
            }

            loginPage.Login(username, password);

            Driver.FindElement(By.LinkText("Shop")).Click();
            var shopPage = new ShopPage(Driver);
            shopPage.AddPoloToCart();

            Driver.FindElement(By.CssSelector("a[href*='cart']")).Click();
            var cartPage = new CartPage(Driver);
            cartPage.ProceedToCheckout();

            //cmplete checkout process and place order
            var checkoutPage = new CheckoutPage(Driver);
            checkoutPage.CompleteBillingDetails(
                firstName: "Jane",
                lastName: "Doe",
                address: "123 Street",
                city: "Testcity",
                postcode: "TF29FT",
                phoneNumber: "07123456789"
            );

            //select payment method and place order
            checkoutPage.SelectPaymentMethod("Cash on delivery");
            string orderNumber = checkoutPage.PlaceOrder();

            //verify order appears in order history
            var accountPage = new AccountPage(Driver);
            accountPage.NavigateToOrders();
            Assert.IsTrue(accountPage.IsOrderPresent(orderNumber), $"Order {orderNumber} not found in order history.");

            //log out to clean up session
            accountPage.Logout();
        }
    }
}
