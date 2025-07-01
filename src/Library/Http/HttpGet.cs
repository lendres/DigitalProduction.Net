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

    public static async Task FileDownload(string fileUrl, string destinationPath)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        var page = await browser.NewPageAsync();


		// METHOD 1.
		//var download = await page.RunAndWaitForDownloadAsync(async () =>
		//{
		//    await page.GotoAsync(fileUrl);
		//});



		// METHOD 2.
		// Go to the download page.
		await page.GotoAsync(fileUrl);
		var downloadTask = page.WaitForDownloadAsync();

		// Simulate a click if necessary (you may need to inspect the page to get the correct selector)
        //await page.ClickAsync("a:has-text('Download')"); // <-- Change this to match your actual page

		var download = await downloadTask;




		// Save file.
        await download.SaveAsAsync(destinationPath);
    }

    public static async Task FileDownloadFromDirectLink(string fileUrl, string destinationPath)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            AcceptDownloads = true,
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 " +
                        "(KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36",
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        });

        var page = await context.NewPageAsync();

        // Wait for the download to happen from a direct navigation
        var download = await page.RunAndWaitForDownloadAsync(() =>
            page.GotoAsync(fileUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle })
        );


		// Save file.
        await download.SaveAsAsync(destinationPath);
    }

    public static async Task FileDownloadFromDirectLink2(string fileUrl, string destinationPath)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            AcceptDownloads = true
        });

        var page = await context.NewPageAsync();

        // Navigate to homepage first to let it solve Cloudflare (optional but safer)
        await page.GotoAsync("https://onepetro.org");

        // Now wait for download from your citation URL
        var download = await page.RunAndWaitForDownloadAsync(() =>
            page.GotoAsync(fileUrl, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle })
        );

        await download.SaveAsAsync(destinationPath);
    }

    public static async Task FileDownloadFromDirectLink3(string fileUrl, string destinationPath)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });

        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        // Go to home page to satisfy Cloudflare or cookies
        await page.GotoAsync("https://onepetro.org");

        // Fetch the file using JS and get it as base64
        string base64 = await page.EvaluateAsync<string>(@"
            async function() {
                const response = await fetch('" + fileUrl + @"');
                if (!response.ok) throw new Error('HTTP error ' + response.status);
                const buffer = await response.arrayBuffer();
                return btoa(String.fromCharCode(...new Uint8Array(buffer)));
            }
        ");

        // Decode base64 and save to file
        byte[] fileBytes = Convert.FromBase64String(base64);
        await File.WriteAllBytesAsync(destinationPath, fileBytes);
    }


    private static readonly string StatePath = Path.Combine(Directory.GetCurrentDirectory(), "auth-state.json");

    public static async Task FileDownloadLogin(string fileUrl, string destinationPath)
    {
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false // Set to false for manual login if needed
        });

        BrowserNewContextOptions contextOptions = new()
        {
            AcceptDownloads = true,
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 " +
                        "(KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36",
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        };

        // Load saved login state if available
        if (File.Exists(StatePath))
        {
            contextOptions.StorageStatePath = StatePath;
        }

        var context = await browser.NewContextAsync(contextOptions);
        var page = await context.NewPageAsync();

        // If this is the first time, login manually and save state
        if (!File.Exists(StatePath))
        {
            Console.WriteLine("Navigate to login page and authenticate manually...");
            await page.GotoAsync("https://onepetro.org"); // ← Replace with actual login page
            Console.WriteLine("Press Enter after completing login...");
            await context.StorageStateAsync(new BrowserContextStorageStateOptions { Path = StatePath });
            Console.WriteLine("Login state saved.");
        }

        // Now go to the download link and download the file
        var downloadTask = page.RunAndWaitForDownloadAsync(async () =>
        {
            await page.GotoAsync(fileUrl);
        });

        var download = await downloadTask;
        await download.SaveAsAsync(destinationPath);

        Console.WriteLine($"File downloaded to: {destinationPath}");
        await browser.CloseAsync();
    }

	#endregion

} // End class.