using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiAutomationFramework.Framework.TestSuite
{
    /// <summary>
    /// BaseTestSuite.TableValidation provides generic and reusable methods for checking web element table, row or columns.
    /// </summary>
    public abstract partial class BaseTestSuite
    {

        /// <summary>
        /// Verifies the table's headers on a specific page.
        /// </summary>
        /// <param name="header">the table header web element</param>
        /// <param name="expectedHeader">an array of expected headers</param>
        /// <param name="pageName">the page name</param>        
        protected void VerifyTableHeaderOnPage(IWebElement header, String[] expectedHeader, String pageName)
        {
            IsElementDisplayed(header, "Table Header", pageName);
            IReadOnlyCollection<IWebElement> headers = header.FindElements(By.TagName("th"));
            Assert.AreEqual(headers.Count, expectedHeader.Length, "The header length is not the expected.");
            for (int i = 0; i < headers.Count; i++)
            {
                IWebElement element = headers.ElementAt(i);
                if (element.Displayed)
                {
                    IsTextPresent(headers.ElementAt(i), expectedHeader[i], "TableHeader::" + expectedHeader[i], pageName);
                }
            }
        }

        /// <summary>
        /// Verifies the table's body on a specific page.
        /// </summary>
        /// <param name="body">the table body web element</param>
        /// <param name="expectedRows">the expected rows</param>
        /// <param name="pageName">the page name</param>
        protected void VerifyTableBodyOnPage(IWebElement body, int expectedRows, String pageName)
        {
            IsElementDisplayed(body, "Table Body", pageName);
            IReadOnlyCollection<IWebElement> bodyRows = body.FindElements(By.TagName("tr"));
            Assert.True(bodyRows.Count >= expectedRows, "The current rows are not equals or higher than the expected rows on " + pageName);
            for (int i = 0; i < bodyRows.Count; i++)
            {
                IWebElement elementRow = bodyRows.ElementAt(i);
                if (elementRow.Displayed)
                {
                    IReadOnlyCollection<IWebElement> bodyRowColumns = elementRow.FindElements(By.TagName("td"));
                    Assert.True(bodyRowColumns.Count > 0, "The current columns value for row " + i + " are zero on " + pageName);
                    for (int j = 0; j < bodyRowColumns.Count; j++)
                    {
                        IWebElement elementColumn = bodyRowColumns.ElementAt(j);
                        if (elementColumn.Displayed)
                        {
                            String rowColumnMessage = "[row:" + i + "|column:" + j + "]";
                            Assert.NotNull(elementColumn, "The " + rowColumnMessage + " is null on " + pageName);
                            Assert.NotNull(elementColumn.Text, "The " + rowColumnMessage + " text is null on " + pageName);
                            Assert.True(elementColumn.Text.Length >= 0, "The " + rowColumnMessage + " text is empty on " + pageName);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Verifies if the table contains a row with expected cell values.
        /// </summary>
        /// <param name="table">table web element</param>
        /// <param name="cellValues">a list of expected cell values</param>
        /// <param name="pageName">the page name</param>    
        /// <returns>True if Row is found/False if Row is not found</returns>
        protected bool VerifyTableRow(IWebElement table, List<String> cellValues, String pageName)
        {
            IsElementDisplayed(table, "Table", pageName);
            IReadOnlyCollection<IWebElement> row = table.FindElements(By.TagName("tr"));
            for (int rowNo = 0; rowNo < row.Count; rowNo++)
            {
                IWebElement element = row.ElementAt(rowNo);
                bool IsCellValuePresentInRow = true;
                for (int i = 0; i < cellValues.Count; i++)
                {
                    if (!element.Text.ToLower().Contains(cellValues[i].ToLower()))
                    {
                        IsCellValuePresentInRow = false;
                    }
                }
                if (IsCellValuePresentInRow)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
