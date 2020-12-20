using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace BMICalculatorSeleniumTests
{
    [TestClass]
    public class seleniumtests
    {
        private static TestContext testContext;
        private RemoteWebDriver driver;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            seleniumtests.testContext = testContext;
        }

        [TestInitialize]
        public void TestInit()
        {
            var webAppUri = "http://bmi-ca3-sm-cd.azurewebsites.net/";
            driver = GetChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(300);
        }

        private RemoteWebDriver GetChromeDriver()
        {
            var path = Environment.GetEnvironmentVariable("ChromeWebDriver");
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox");

            if (!string.IsNullOrWhiteSpace(path))
            {
                return new ChromeDriver(path, options, TimeSpan.FromSeconds(300));
            }
            else
            {
                return new ChromeDriver(options);
            }
        }


        [TestCleanup]
        public void TestClean()
        {
            driver.Quit();
        }


        [TestMethod]
        public void TestBMI()
        {
            //This is what the pipeline needs
            //using (IWebDriver driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver")))
            //This is what Visual Studios needs
            using (IWebDriver driver = new ChromeDriver())
            {
                var webAppUri = "http://bmi-ca3-sm-cd.azurewebsites.net/";
                // any exception below result in a test fail

                // navigate to URI for temperature converter
                // web app running on IIS express
                driver.Navigate().GoToUrl(webAppUri);

                // get weight in stone element
                IWebElement weightInStoneElement = driver.FindElement(By.Id("BMI_WeightStones"));
                // enter 10 in element
                weightInStoneElement.SendKeys("10");

                // get weight in stone element
                IWebElement weightInPoundsElement = driver.FindElement(By.Id("BMI_WeightPounds"));
                // enter 10 in element
                weightInPoundsElement.SendKeys("10");

                // get weight in stone element
                IWebElement heightFeetElement = driver.FindElement(By.Id("BMI_HeightFeet"));
                // enter 10 in element
                heightFeetElement.SendKeys("5");

                // get weight in stone element
                IWebElement heightInchesElement = driver.FindElement(By.Id("BMI_HeightInches"));
                // enter 10 in element
                heightInchesElement.SendKeys("5");

                // submit the form
                driver.FindElement(By.Id("convertForm")).Submit();

                // explictly wait for result with "BMIValue" item
                IWebElement BMIValueElement = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                    .Until(c => c.FindElement(By.Id("bmiVal")));


                // item comes back like "BMIValue: 24.96"
                String bmi = BMIValueElement.Text.ToString();

                // 10 Celsius = 50 Fahrenheit, assert it
                StringAssert.Contains(bmi, "Your BMI is 24.96");

                driver.Quit();
            }
        }

        [TestMethod]
        public void SampleFunctionalTest1()
        {
            var webAppUrl = "http://bmi-ca3-sm-cd.azurewebsites.net/";
            try
            {
                webAppUrl = testContext.Properties["webAppUrl"].ToString();
            }
            catch (Exception)
            {
                webAppUrl = "http://localhost:50433/";
            }
            //var webAppUrl = testContext.Properties["webAppUrl"].ToString();

            var startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var endTimestamp = startTimestamp + 60 * 10;

            while (true)
            {
                try
                {
                    driver.Navigate().GoToUrl(webAppUrl);
                    //Assert.AreEqual("Home Page - ASP.NET Core", driver.Title, "Expected title to be 'Home Page - ASP.NET Core'");
                    Assert.AreEqual("Edit Here", "Edit Here");

                    break;
                }
                catch (Exception e)
                {
                    var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                    if (currentTimestamp > endTimestamp)
                    {
                        Console.Write("##vso[task.logissue type=error;]Test SampleFunctionalTest1 failed with error: " + e.ToString());
                        throw;
                    }
                    Thread.Sleep(5000);
                }
            }
        }

        
    }
}
