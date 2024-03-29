﻿using Blaise.Tests.Helpers.Browser;
using Blaise.Tests.Helpers.Instrument;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Blaise.Tests.Helpers.Configuration;
using System;

namespace Blaise.Tests.Helpers.ErrorHandler
{
    [Binding]
    public sealed class CommonHooks
    {
        private readonly ScenarioContext _scenarioContext;


        public CommonHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void CheckForErroneousInstrument()
        {
            InstrumentHelper.GetInstance().CheckForErroneousInstrument(BlaiseConfigurationHelper.InstrumentName);
        }


        [AfterStep]
        public void OnError()
        {
            if (_scenarioContext.TestError != null)
            {
                BrowserHelper.OnError(TestContext.CurrentContext, _scenarioContext);
                throw new Exception(_scenarioContext.TestError.Message);
            }
        }
    }
}
