using HtmlAgilityPack;
using Microsoft.Playwright;

namespace DigitalProduction.Http;

/// <summary>
/// 
/// </summary>
public class HttpGet
{
	#region Fields

	#endregion

	#region Construction

	/// <summary>
	/// Default constructor.
	/// </summary>
	public HttpGet()
	{
	}

	#endregion

	#region Properties

	#endregion

	#region Methods

	/// <summary>
	/// Gets all hyperref links on a page.  Extracts the content and "href" of an "a" element.
	/// </summary>
	/// <param name="url">URL of page to extract the links from.</param>
	public static List<string[]> GetAllLinksOnPage(string url)
	{
		HtmlDocument htmlDocument			= new HtmlWeb().Load(url);
		IEnumerable<HtmlNode> linkedPages	= htmlDocument.DocumentNode.Descendants("a")
			.Select(a => a)
			.Where(u => !String.IsNullOrEmpty(u.GetAttributeValue("href", null)) & !String.IsNullOrEmpty(u.InnerHtml));

		List<string[]> links = new();

		foreach (HtmlNode link in linkedPages)
		{
			links.Add(new string[] { link.InnerHtml, link.GetAttributeValue("href", null) });
		}

		return links;
	}

    public static async Task DownloadWithPlaywrightAsync(string fileUrl, string destinationPath)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        var page = await browser.NewPageAsync();
        var download = await page.RunAndWaitForDownloadAsync(async () =>
        {
            await page.GotoAsync(fileUrl);
        });

        await download.SaveAsAsync(destinationPath);
        Console.WriteLine("Downloaded file to: " + destinationPath);
    }

	#endregion

} // End class.