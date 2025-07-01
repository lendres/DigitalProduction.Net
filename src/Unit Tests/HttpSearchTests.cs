using DigitalProduction.Http;
using Google.Apis.CustomSearchAPI.v1.Data;
using Microsoft.Playwright;
using static System.Net.WebRequestMethods;

namespace DigitalProduction.UnitTests;

/// <summary>
/// Test cases the the Mathmatics namespace.
/// </summary>
public class HttpSearchTests
{
	#region Fields

	#endregion

	#region Construction

	public HttpSearchTests()
	{
		CustomSearchKey? customSearchKey = CustomSearchKey.Deserialize(@"..\..\..\..\..\..\Keys\Google\customsearchkey.xml");
		Assert.NotNull(customSearchKey);
		
		CustomSearch.SetCxAndKey(customSearchKey);
	}

	#endregion

	#region Tests

	/// <summary>
	/// Covariance test.
	/// </summary>
	[Fact]
	public void SearchTest1()
	{
		string search1 = "A Novel Approach To Borehole Quality Measurement In Unconventional Drilling";
		//string search2 = "Proven Well Stabilization Technology for Trouble-Free Drilling and Cost Savings in Pressurized Permeable Formations";
		string searchTerms = search1;

		IList<Result> results = CustomSearch.Search(searchTerms);

		string resultString = "Search: " + searchTerms + Environment.NewLine + Environment.NewLine;
		foreach (Result result in results)
		{
			resultString += ResultString(result) + Environment.NewLine + Environment.NewLine + Environment.NewLine;
		}

		//Assert.AreEqual(Statistics.Covariance(xValues, yValues), 2.9167, 0.0001, errorMessage);
	}

	private static string ResultString(Result result)
	{
		string resultString = "";
		resultString += "Title:        " + result.Title + Environment.NewLine;
		resultString += "URL:          " + result.FormattedUrl + Environment.NewLine;
		resultString += "HTML Title:   " + result.HtmlTitle + Environment.NewLine;
		resultString += "Display Link: " + result.DisplayLink + Environment.NewLine;
		resultString += "Link:         " + result.Link + Environment.NewLine;
		resultString += "HtmlSnippet:  " + result.HtmlSnippet + Environment.NewLine;
		resultString += "Snippet:      " + result.Snippet + Environment.NewLine;
		//resultString += ": " + result + Environment.NewLine;
		return resultString;
	}

	/// <summary>
	/// Covariance test.
	/// </summary>
	[Fact]
	public void SiteSearchTest1()
	{
		string search1		= "Improving Casing Running Efficiency Through A Comprehensive Wellbore Quality Scorecard: A Datadriven Approach";
		//string search2 = "Proven Well Stabilization Technology for Trouble-Free Drilling and Cost Savings in Pressurized Permeable Formations";
		string searchTerms	= search1;
		string website      = "onepetro.org";

		IList<Result>? results = CustomSearch.SiteSearch(searchTerms, website, 0);
		Assert.NotNull(results);

		string resultString = "Search: " + searchTerms + Environment.NewLine + Environment.NewLine;
		foreach (Result result in results)
		{
			resultString += ResultString(result) + Environment.NewLine + Environment.NewLine + Environment.NewLine;
		}
	}

	/// <summary>
	/// Covariance test.
	/// </summary>
	[Fact]
	public async Task FileDownloadTest()
	{

		string downloadUrl;
		downloadUrl = "https://onepetro.org/Citation/Download?resourceId=542925&resourceType=3&citationFormat=2";
		//downloadUrl = "http://ipv4.download.thinkbroadband.com:8080/10MB.zip";
		//downloadUrl = "http://212.183.159.230/5MB.zip";
		//downloadUrl = "https://examplefile.com/file-download/19";

		await HttpGet.FileDownloadLogin(downloadUrl, "C:\temp\fildownload.txt");
	}



	/// <summary>
	/// Covariance test.
	/// </summary>
	[Fact]
	public async Task SaveLoginTest()
	{
		await SaveLoginStateAsync2();

    }

	public static async Task SaveLoginStateAsync()
	{
		using var playwright = await Playwright.CreateAsync();
		await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

		var context = await browser.NewContextAsync();
		var page = await context.NewPageAsync();

		Console.WriteLine("Log in manually in the browser window...");

		await page.GotoAsync("https://onepetro.org");

		// Give you time to login (manually or institutionally)
		Console.WriteLine("Press ENTER once you're logged in.");
		Console.ReadLine();

		await context.StorageStateAsync(new BrowserContextStorageStateOptions
		{
			Path = "auth.json"
		});

		Console.WriteLine("Login state saved.");
	}

public static async Task SaveLoginStateAsync2()
{
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
    {
        Headless = false,   // <-- Required for Cloudflare detection to treat this as human
        SlowMo = 50         // (optional) slows interactions for better manual login
    });

    var context = await browser.NewContextAsync();
    var page = await context.NewPageAsync();

    Console.WriteLine("Navigate to OnePetro and complete all human checks and logins.");

    await page.GotoAsync("https://onepetro.org");

    Console.WriteLine("Press ENTER once Cloudflare and login are complete...");
    Console.ReadLine();

    await context.StorageStateAsync(new BrowserContextStorageStateOptions
    {
        Path = "auth.json"
    });

    Console.WriteLine("Saved authenticated session.");
}


	#endregion

} // End class.