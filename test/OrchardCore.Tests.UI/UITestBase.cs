using System;
using System.Threading.Tasks;
using Lombiq.Tests.UI;
using Lombiq.Tests.UI.Helpers;
using Lombiq.Tests.UI.Services;
using OrchardCore.Tests.UI.Helpers;
using Xunit.Abstractions;

namespace OrchardCore.Tests.UI
{
    public class UITestBase : OrchardCoreUITestBase
    {
        protected override string AppAssemblyPath => WebAppConfigHelper
            .GetAbsoluteApplicationAssemblyPath("OrchardCore.Cms.Web", "net6.0");

        protected UITestBase(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        protected override Task ExecuteTestAfterSetupAsync(
            Func<UITestContext, Task> testAsync,
            Browser browser,
            Func<OrchardCoreUITestExecutorConfiguration, Task> changeConfigurationAsync) =>
            ExecuteTestAsync(testAsync, browser, SetupHelpers.RunSetupAsync, changeConfigurationAsync);

        protected override Task ExecuteTestAsync(
            Func<UITestContext, Task> testAsync,
            Browser browser,
            Func<UITestContext, Task<Uri>> setupOperation,
            Func<OrchardCoreUITestExecutorConfiguration, Task> changeConfigurationAsync) =>
            base.ExecuteTestAsync(
                testAsync,
                browser,
                setupOperation,
                async configuration =>
                {
                    if (changeConfigurationAsync != null) await changeConfigurationAsync(configuration);
                });
    }
}