using DriverLibrary;
using System;

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
            ClickBelgiumFilter();
            ResetBelgiumFilter();

            VerifySearchBoxIsPresent();
            SearchForActivisionBlizzard();
            //Closing the report
            Global.report.EndReport();

            //Closing the connection to the driver
            Global.driver.TearDown();
        }

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

        public static void ClickBelgiumFilter()
        {
            //Creating a test case object to store the details of each test
            TestCase test = new TestCase("Verify filter for Belgium works", "The only items in the list have the value Belgium in the Country cell");
            Global.driver.ClickElementByID(PageObjectModel.belgiumCheckbox);
            Global.driver.ClickElementByClass("header");
            Global.driver.PressDownArrow();
            Global.driver.ClickElementByID(PageObjectModel.updateButton, "Filtered By Country Belgium", arrayIndex: 1);
            
            for(int i=0; i<Global.driver.GetRowCount(); i++)
            {
                if(!Global.driver.GetCellContentByRowAndColumn(i).Equals("Belgium"))
                {
                    test.SetActualOutcome("List was not properly filtered");
                    test.SetTestResult(false);
                }
                else
                {
                    test.SetActualOutcome("The list was properly filtered");
                    test.SetTestResult(true);
                }
            }
            Global.report.AddTestCaseToReport(test);
        }


        public static void ResetBelgiumFilter()
        {
            //Creating a test case object to store the details of each test
            TestCase test = new TestCase("Verify resetting filter for Belgium works", "The filter should be resest");
            Global.driver.ClickElementByID(PageObjectModel.belgiumCheckbox);
            Global.driver.ClickElementByClass("header");
            Global.driver.PressDownArrow();
            Global.driver.ClickElementByID(PageObjectModel.resetButton, "Filtered By Country Belgium Reset", arrayIndex: 1);
            
            for (int i = 0; i < Global.driver.GetRowCount(); i++)
            {
                if (!Global.driver.GetCellContentByRowAndColumn(i).Equals("Belgium"))
                {
                    test.SetActualOutcome("The list was properly reset");
                    test.SetTestResult(true);
                }
                else
                {
                   
                    test.SetActualOutcome("List was not properly reset");
                    test.SetTestResult(false);
                }
            }
            Global.report.AddTestCaseToReport(test);
        }

        public static void VerifySearchBoxIsPresent()
        {
            //Creating a test case object to store the details of each test
            TestCase test = new TestCase("Verify search comboBox has loaded", "Page should load and search box should be present");
            if (Global.driver.IsElementPresentByID(PageObjectModel.searchComboBox))
            {
                test.SetActualOutcome("Page has loaded and search box is present");
                test.SetTestResult(true);
            }
            else
            {
                test.SetActualOutcome("Search box is not present or the id has changed. Page may not have loaded properly");
                test.SetTestResult(false);
            }
            Global.report.AddTestCaseToReport(test);
        }

        public static void SearchForActivisionBlizzard()
        {
            //Creating a test case object to store the details of each test
            TestCase test = new TestCase("Verify search comboBox works", "Search box should work as expected");
            Global.driver.SendKeysToElementByID(PageObjectModel.searchComboBox, "Activision Blizzard Inc", "Search for Activision Blizzard Inc");
            Global.driver.PressDownArrow();

            if (Global.driver.GetTextFromElementByID(PageObjectModel.issuerName).Equals("Activision Blizzard Inc"))
            {
                test.SetActualOutcome("Search for Activision Blizzard Inc was successful");
                test.SetTestResult(true);
            }
            else
            {
                test.SetActualOutcome("Search for Activision Blizzard Inc was not successful");
                test.SetTestResult(false);
            }
            Global.report.AddTestCaseToReport(test);
        }


    }
}
