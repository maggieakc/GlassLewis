using DriverLibrary;

namespace GlassLewis
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigurationReader config = new ConfigurationReader();

            //Separating the driver functions and the reporting functions into a library to increase the reusabilty of this functionality
            Global.driver = new Driver(Global.browser);
            Global.report = new Report();

            //Assuming the URL does not change and therefore it is safe to hardcode it. If the URL changes then I would move this to an external file e.g XML file and read in the URL
            Global.driver.GoToUrl(Global.url);
            Global.driver.WaitForElementToLoadById(PageObjectModel.countryFilter);
            VerifyBelgiumFilterIsPresent();

            //Closing the report
            Global.report.EndReport();

            //Closing the connection to the driver
            Global.driver.TearDown();
        }


        /// <summary>
        /// When the page loads buttons should be present
        /// </summary>
        public static void VerifyBelgiumFilterIsPresent()
        {
            //Creating a test case object to store the details of each test
            TestCase test = new TestCase("Verify filter for Belgium has loaded", "Page should load and belgium checkbox should be present");
            if (Global.driver.IsElementPresentByID(PageObjectModel.belgiumCheckbox))
            {
                Global.driver.ScreenCapture("Landing Page");
                test.SetActualOutcome("Page has loaded and checkbox is present");
                test.SetTestResult(true);
            }
            else
            {
                test.SetActualOutcome("Checkbox is not present or the id has changed. Page may not have loaded properly");
                test.SetTestResult(false);
            }
            Global.report.AddTestCaseToReport(test);
        }

        /// <summary>
        /// When the page loads buttons should be present
        /// </summary>
        public static void ClickBelgiumFilter()
        {
            //Creating a test case object to store the details of each test
            TestCase test = new TestCase("Verify filter for Belgium has loaded", "Page should load and belgium checkbox should be present");
            Global.driver.ClickElementByID(PageObjectModel.belgiumCheckbox);
            Global.driver.ClickElementByID(PageObjectModel.updateButton, "Filtered By Country Belgium", arrayIndex: 2);

            test.SetActualOutcome("Page has loaded and checkbox is present");
            //test.SetTestResult(true);
            //}
            //else
            //{
            //    test.SetActualOutcome("Checkbox is not present or the id has changed. Page may not have loaded properly");
            //    test.SetTestResult(false);
            //}
            Global.report.AddTestCaseToReport(test);
        }

    }
}
