﻿using System;
using GitHubTests.GitHub.API;
using GitHubTests.GitHub.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GitHubTests
{
    [TestClass]
    public class GitHubUITests
    {
        IWebDriver driver;

        [TestInitialize]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

        [TestCategory("UITest"), TestMethod]
        public void OpenIssuesAreLessThenClosed()
        {
            IssuesPage issues = new IssuesPage(this.driver);
            int open = issues.GetOpenIssuesCount();
            int closed = issues.GetClosedIssuesCount();
            Assert.IsTrue(open < closed, "Open issues are more than opened!");
        }

        [TestCategory("UITest"), TestMethod]
        public void OpenIssuesCountIsCorrect()
        {
            IssuesPage issuesPage = new IssuesPage(this.driver);
            IssuesAPI issuesAPI = new IssuesAPI();
            int openUI = issuesPage.GetOpenIssuesCount();
            int openAPI = issuesAPI.GetAllIssues(organization:"dtopuzov", repository:"test", options: "state=open").Count;
            Assert.AreEqual(openUI, openAPI, "API and UI report different issues count!");
        }
    }
}
