﻿[Binding]
public sealed class CommonHooks
{
    private readonly FeatureContext _featureContext;

    public CommonHooks(FeatureContext featureContext)
    {
        _featureContext = featureContext;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        Console.WriteLine("BeforeTestRun hook is running...");
        CheckForErroneousQuestionnaire();
    }

    [BeforeScenario(Order = -1)]
    public void BeforeScenario()
    {
        Console.WriteLine("BeforeScenario hook is running...");
        CheckForErroneousQuestionnaire();
    }

    [AfterStep]
    public void OnError()
    {
        if (_featureContext.TestError!= null)
        {
            BrowserHelper.OnError(TestContext.CurrentContext, _featureContext);
            throw new Exception(_featureContext.TestError.Message);
        }
    }

    private static void CheckForErroneousQuestionnaire()
    {
        var questionnaireHelper = QuestionnaireHelper.GetInstance();
        var questionnaireStatus = questionnaireHelper.GetQuestionnaireStatus();

        if (questionnaireStatus == QuestionnaireStatusType.Erroneous)
        {
            Console.WriteLine(@"
 ______ _____ _____ ____ _ _ ______ ____ _ _ _____
| ____| __ \| __ \ / __ \| \ | | ____/ __ \| | | |/ ____|
| |__ | |__) | |__) | | | | \| | |__ | | | | | | | (___
| __| | _ /| _ /| | | |. ` | __|| | | | | | |\___ \
| |____| | \ \| | \ \| |__| | |\ | |___| |__| | |__| |____) |
|______|_| \_\_| \_\\____/|_| \_|______\____/ \____/|_____/
");
            Console.WriteLine("The questionnaire is in an erroneous state. All tests are skipped. Please restart Blaise on the management VM and uninstall it via Blaise Server Manager.");
            Assert.Fail("The questionnaire is in an erroneous state. All tests are skipped.");
        }
    }
}
