using OpenQA.Selenium;

public class LoginPage
{
    private readonly IWebDriver _driver; // private variable to hold the WebDriver instance

    //hardcoded credentials (default)
    private const string DefaultUsername = "email@address.com"; //credentials for pre-created account (for the test)
    private const string DefaultPassword = "strong!password";

    public LoginPage(IWebDriver driver) => _driver = driver; //constructor to assign driver

    //method to log in with optional parameters (environment)
    public void Login(string username = null, string password = null)
    {
        //use environment variables if available, otherwise fall back to hardcoded credentials
        //uncomment the lines below to enable environment variable login
        /*
        string userToUse = !string.IsNullOrEmpty(username) ? username : Environment.GetEnvironmentVariable("EMAIL") ?? DefaultUsername;
        string passwordToUse = !string.IsNullOrEmpty(password) ? password : Environment.GetEnvironmentVariable("PASSWORD") ?? DefaultPassword;
        */

        //for this implementation, I used the hardcoded default credentials, because it doesn't work when the computer extracts variables from the envinronment
        //as the task required a pre-established account
        string userToUse = DefaultUsername; //set to be hardcoded credentials
        string passwordToUse = DefaultPassword;

        //navigate to the login page
        _driver.Navigate().GoToUrl("https://www.edgewordstraining.co.uk/demo-site/my-account/");

        //enter the username and password in the login form
        _driver.FindElement(By.Id("username")).SendKeys(userToUse); 
        _driver.FindElement(By.Id("password")).SendKeys(passwordToUse);
        _driver.FindElement(By.CssSelector("button[name='login']")).Click();
    }


}
