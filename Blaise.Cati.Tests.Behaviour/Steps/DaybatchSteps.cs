﻿using System;
using System.Collections.Generic;
using System.Linq;
using Blaise.Tests.Helpers.Browser;
using Blaise.Tests.Helpers.Case;
using Blaise.Tests.Helpers.Cati;
using Blaise.Tests.Helpers.Configuration;
using Blaise.Tests.Helpers.Instrument;
using Blaise.Tests.Models.Case;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Blaise.Cati.Management.Tests.Behaviour.Steps
{
    [Binding]
    public sealed class DaybatchSteps
    {
        [BeforeFeature("cati")]
        public static void InitializeFeature()
        {
            InstrumentHelper.GetInstance().InstallInstrument();
        }

        [Given(@"I log on to Cati as an administrator")]
        public void GivenILogOnToTheCatiDashboard()
        {
            try
            {
                CatiManagementHelper.GetInstance().LogIntoCatiManagementPortal();
                Assert.AreNotEqual(CatiConfigurationHelper.LoginUrl, CatiManagementHelper.GetInstance().CurrentUrl(),
                    "Expected to leave the login page");
            }
            catch (Exception e)
            {
                FailWithScreenShot(e,"LogOnCati", "Log onto Cati as Admin");
            }
        }

        [Given(@"I have created a daybatch for today")]
        [When(@"I create a daybatch for today")]
        public void WhenICreateADaybatchForToday()
        {
            try
            {
                CatiManagementHelper.GetInstance().CreateDayBatch();
            }
            catch (Exception e)
            {
                FailWithScreenShot(e, "CreateDaybatch", "Create a daybatch for today");
            }
        }

        [Then(@"the sample cases are present on the daybatch entry screen")]
        public void ThenTheSampleCasesArePresentOnTheDaybatchEntryScreen(IEnumerable<CaseModel> cases)
        {
            try
            {
                var entriesText = CatiManagementHelper.GetInstance().GetDaybatchEntriesText();
                var expectedNumberOfCases = cases.Count();
                Assert.AreEqual($"Showing 1 to {expectedNumberOfCases} of {expectedNumberOfCases} entries", entriesText);
            }
            catch (Exception e)
            {
                FailWithScreenShot(e, "SampleCases", "Daybatch entry screen");
            }
        }
                
        [AfterFeature("cati")]
        public static void CleanUpFeature()
        {
            CatiManagementHelper.GetInstance().ClearDayBatchEntries();
            BrowserHelper.CloseBrowser();
            CaseHelper.GetInstance().DeleteCases();
            InstrumentHelper.GetInstance().UninstallSurvey();
        }

        private static void FailWithScreenShot(Exception e, string screenShotName, string screenShotDescription)
        {
            var screenShotFile = BrowserHelper.TakeScreenShot(TestContext.CurrentContext.WorkDirectory,
                screenShotName);

            TestContext.AddTestAttachment(screenShotFile, screenShotDescription);
            Assert.Fail($"The test failed to complete - {e.Message}");
        }
    }
}