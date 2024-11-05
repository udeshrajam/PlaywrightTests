

using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightTests.Utilities;
using System.Text.Json;
using System.Threading.Tasks;


namespace PlaywrightTests
{
    class Accessibility
    {
        private IPage _page;

        public Accessibility(IPage page)
        {
            _page = page;
        }


        public async Task AnalyzeAccessibilityAsync()
        {
            try
            {
                string axeScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "node_modules", "axe-core", "axe.min.js");

                if (!File.Exists(axeScriptPath))
                {
                    Console.WriteLine($"Axe-Core script not found at {axeScriptPath}. Ensure axe-core is installed correctly.");
                    return;
                }

                string axeScriptContent;
                try
                {
                    axeScriptContent = File.ReadAllText(axeScriptPath);
                    Console.WriteLine("Axe-Core script content successfully read.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading Axe-Core script synchronously: {ex.Message}");
                    return;
                }

                try
                {

                    await _page.AddScriptTagAsync(new PageAddScriptTagOptions { Content = axeScriptContent });
                    Console.WriteLine("Axe-core script injected using the file path.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Axe-core script injected using the file path.: {ex.Message}");
                    return;
                }




                var result = await _page.EvaluateAsync<JsonElement>(@"async () => {return await axe.run();}");


                var violations = result.GetProperty("violations");
                if (violations.GetArrayLength() > 0)
                {
                    foreach (var violation in violations.EnumerateArray())
                    {
                        var description = violation.GetProperty("description").GetString();
                        var nodes = violation.GetProperty("nodes");
                        Console.WriteLine($"Violation: {description}");
                        foreach (var node in nodes.EnumerateArray())
                        {
                            var target = node.GetProperty("target")[0].GetString();
                            Console.WriteLine($"  Affected element: {target}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No accessibility violations found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}