using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace GlassLewis
{
    public class ConfigurationReader
    {
        //Declare the path to the configuration file
        string settings = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xml");

        /// <summary>
        /// Populate the Global variables with the values for the test run. These values are read from the MvisionSettings.XML file located beside the running executable.
        /// </summary>
        public ConfigurationReader()
        {
            //Set the Global variable MVisionUrl using configuration file value - value is called PBC Url in the configuration file due to a bug in Akeso
            Global.Url = getStringSetting("Environment", "Url");
        }

        /// <summary>
        /// Gets string value for specific setting in the configuration file
        /// </summary>
        /// <param name="settingGroup">Name of the setting group in the configuration file</param>
        /// <param name="settingName">Name of the setting in the configuration file</param>
        /// <param name="settingValue">Name of the setting value in the configuration. Default is "Value"</param>
        /// <returns></returns>
        private string getStringSetting(string settingGroup, string settingName, string settingValue = "Value")
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(settings);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (!string.IsNullOrEmpty(node.Name) && node.Name.Equals(settingGroup))
                {
                    foreach (XmlNode childNode in node)
                    {
                        if (!string.IsNullOrEmpty(childNode.Name) && childNode.Attributes["Name"].Value.Equals(settingName))
                        {
                            return childNode.Attributes[settingValue].Value;
                        }
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets a list of string for specific settingGroup name from configuration file
        /// </summary>
        /// <param name="settingGroup"></param>
        /// <returns></returns>
        private List<string> getListSetting(string settingGroup)
        {
            List<string> testList = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(settings);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (!string.IsNullOrEmpty(node.Name) && node.Name.Equals(settingGroup))
                {
                    foreach (XmlNode childNode in node)
                    {
                        testList.Add(childNode.Attributes["Name"].Value);
                    }
                }
            }
            return testList;
        }
    }
}
