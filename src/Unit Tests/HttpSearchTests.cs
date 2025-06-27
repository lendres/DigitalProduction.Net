using DigitalProduction.Http;
using Google.Apis.CustomSearchAPI.v1.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		string search1 = "Pilot Bit and Reamer Matching: Real-Time Downhole Data Differentiates Hybrid Drill Bit’s Suitability with Concentric Reamer in Deepwater, Gulf of Mexico Application";
		//string search2 = "Proven Well Stabilization Technology for Trouble-Free Drilling and Cost Savings in Pressurized Permeable Formations";
		string searchTerms = search1;
		string website      = "onepetro.org";

		IList<Result> results = CustomSearch.SiteSearch(searchTerms, website, 0);

		string resultString = "Search: " + searchTerms + Environment.NewLine + Environment.NewLine;
		foreach (Result result in results)
		{
			resultString += ResultString(result) + Environment.NewLine + Environment.NewLine + Environment.NewLine;
		}

		//Assert.AreEqual(Statistics.Covariance(xValues, yValues), 2.9167, 0.0001, errorMessage);
	}

	#endregion

} // End class.