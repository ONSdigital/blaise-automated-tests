﻿using System.Collections.Generic;
using Blaise.Tests.Models.Case;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Blaise.Tests.Behaviour.Transforms
{
    [Binding]
    public class CaseStepArgumentTransformations
    {
        [StepArgumentTransformation]
        public IEnumerable<CaseModel> TransformCaseTableIntoListCaseModel(Table table)
        {
            return table.CreateSet<CaseModel>();
        }
    }
}
