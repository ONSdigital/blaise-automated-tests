﻿using Blaise.Nuget.Api.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaise.Smoke.Tests.Helpers
{
    public class ConfigurationHelper
    {
        public string BlaiseServerHostName => GetVariable("BlaiseServerHostName");
        public string BlaiseServerUserName => GetVariable("BlaiseServerUserName");
        public string BlaiseServerPassword => GetVariable("BlaiseServerPassword");
        public string BlaiseServerBinding => GetVariable("BlaiseServerBinding");
        public string ServerParkName => GetVariable("ServerParkName");
        public int BlaiseConnectionPort => GetIntVariable("BlaiseConnectionPort");
        public int BlaiseRemoteConnectionPort => GetIntVariable("BlaiseRemoteConnectionPort");
        public int ConnectionExpiresInMinutes => GetIntVariable("ConnectionExpiresInMinutes");
        public string InsturmentPath => GetVariable("InstrumentPath");
        public string CatiUsername => GetVariable("CatiUsername");
        public string CatiPassword => GetVariable("CatiPassword");
        public string LoginURL => GetVariable("LoginURL");
        public string CaseInfoURL => GetVariable("CaseInfoURL");
        public string DayBatchURL => GetVariable("DayBatchURL");
        public string InterviewURL => GetVariable("InterviewURL");
        public string SpecificationURL => GetVariable("SpecificationURL");
        public string SurveyURL => GetVariable("SurveyURL");
        public string ChromeDriver => GetVariable("ChromeDriver");

        private static string GetVariable(string variableName)
        {
            var value = ConfigurationManager.AppSettings[variableName];
            return value;
        }

        private static int GetIntVariable(string variableName)
        {
            var value = ConfigurationManager.AppSettings[variableName];
            return Convert.ToInt32(value);
        }

        public ConnectionModel BuildConnectionModel()
        {
            return new ConnectionModel{
                ServerName = BlaiseServerHostName,
                UserName = BlaiseServerUserName,
                Password = BlaiseServerPassword,
                Binding = BlaiseServerBinding,
                Port = BlaiseConnectionPort,
                RemotePort = BlaiseRemoteConnectionPort,
                ConnectionExpiresInMinutes = ConnectionExpiresInMinutes
            };
        }
    }
}
