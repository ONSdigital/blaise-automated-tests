﻿using System.Collections.Generic;
using Blaise.Tests.Helpers.Configuration;
using Blaise.Tests.Helpers.Framework;

namespace Blaise.Tests.Helpers.Dqs.Pages
{
    public class HomePage : BasePage
    {
        private const string DeployQuestionnaireButtonId = "deploy-questionnaire-link";
        public string QuestionnaireTableId = "instrument-table";
        public string QuestionnaireTableRowsPath = "//*[@id='instrument-table']/tbody/tr";
        private const string SummaryDivPath = "//div[contains(@class, 'success panel')]";
        public string InfoButtonPlaceholderId = "info-";
        public string FilterId = "filter-by-name";

        public HomePage() : base(DqsConfigurationHelper.DqsUrl)
        {
        }

        public void ClickDeployAQuestionnaire()
        {
            ClickButtonById(DeployQuestionnaireButtonId);
        }

        public List<string> GetFirstColumnFromTableContent()
        {
            var elements = GetFirstColumnOfTableFromXPath(QuestionnaireTableRowsPath, QuestionnaireTableId);
            return elements;
        }
        
        public string GetUploadSummaryText()
        {
            return GetElementTextByPath(SummaryDivPath);
        }

        public void ClickInstrumentInfoButton(string instrumentName)
        {
            ClickButtonById(InfoButtonPlaceholderId + instrumentName);
        }

        public void FilterInstruments(string instrumentName)
        {
            PopulateInputById(FilterId, instrumentName);
        }
    }
}
