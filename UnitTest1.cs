using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumCSharpStarterTut
{
	public class Tests
	{
		private WebDriver WebDriver { get; set; }

		private string DriverPath { get; set; }

		private string BaseUrl { get; set; } = "https://www.blazingchat.com";


		[SetUp]
		public void Setup()
		{
			DriverPath = GetChromeDriverPath();

			WebDriver = GetChromeDriver();

			// wait for 120 seconds for the broswer to start
			WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
		}

		[TearDown]
		public void TearDown()
		{
			WebDriver.Quit();
		}

		[Test]
		public void Test1()
		{
			WebDriver.Navigate().GoToUrl(BaseUrl);
			//Assert.AreEqual("BlazingChat", WebDriver.Title);
			Assert.That(WebDriver.Title, Is.EqualTo("BlazingChat"));

		}

		[Test]
		public void Test2()
		{

			WebDriver.Navigate().GoToUrl(BaseUrl);
			//email
			var input = WebDriver.FindElement(By.Id("input_emailaddress"));
			input.Clear();
			input.SendKeys("john.smith@gmail.com");
			Thread.Sleep(2000);

			//password
			var passwordElemenet = WebDriver.FindElement(By.Id("input_password"));
			passwordElemenet.Clear();
			passwordElemenet.SendKeys("john.smith");
			Thread.Sleep(2000);

			//login button
			var loginButtonElement = WebDriver.FindElement(By.Id("button_login"));
			loginButtonElement.Click();


			//validate login message
			var startTimeStamp = DateTime.Now.Millisecond;
			var endTimeStamp = startTimeStamp + 15000;

			while (true)
			{
				try
				{
					//check if sucessfully logged in or not
					var welcomeMessage = WebDriver.FindElement(By.Id("p_welcome_message"));
					Assert.That(welcomeMessage.Text, Is.EqualTo("Hello, john.smith@gmail.com"));
					break;
				}
				catch
				{
					var currentTimeStamp = DateTime.Now.Millisecond;
					if (currentTimeStamp > endTimeStamp)
					{
						throw;
					}
					Thread.Sleep(2000);
				}
			}

		}

		private WebDriver GetChromeDriver()
		{
			var options = new ChromeOptions();

			return new ChromeDriver(DriverPath, options, TimeSpan.FromSeconds(300));
		}

		private string GetChromeDriverPath()
		{
			return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resources"));
		}
	}
}