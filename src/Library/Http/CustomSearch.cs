using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.CustomSearchAPI.v1.Data;
using Google.Apis.Services;

namespace DigitalProduction.Http;

/// <summary>
/// A custom Google search that uses the Google API.
/// </summary>
public static class CustomSearch
{
	#region Fields

	// The custom search engine identifier.
	private static string						_cx				= "";

	// API Key.
	private static string						_apiKey			= "";

	// The seach service.
	private static CustomSearchAPIService?		_service		= null;

	#endregion

	#region Properties

	/// <summary>
	/// The custom search engine identifier (cx).
	/// </summary>
	public static string Cx { get => _cx; private set => _cx = value; }

	/// <summary>
	/// API Key.
	/// </summary>
	public static string ApiKey
	{
		get => _apiKey;
		private set
		{
			_apiKey = value;
			CreateService();
		}
	}

	/// <summary>
	/// Gets the search service.
	/// </summary>
	public static CustomSearchAPIService Service
	{
		get
		{
			if (_service == null)
			{
				throw new Exception("The key and search engine identifier (cx) must be set before accessing the service.");
			}
			return _service;
		}
	}


	#endregion

	#region Methods

	/// <summary>
	/// Sets the API key and custom search engine identifier and initializes the service.
	/// </summary>
	/// <param name="apiKey">API key.</param>
	/// <param name="cx">Custom search engine identifier.</param>
	public static void SetCxAndKey(string cx, string apiKey)
	{
		CustomSearch.Cx		= cx;
		CustomSearch.ApiKey	= apiKey;
	}

	/// <summary>
	/// Sets the API key and custom search engine identifier and initializes the service.
	/// </summary>
	/// <param name="customSearchKey">Custom search key object.</param>
	public static void SetCxAndKey(CustomSearchKey customSearchKey)
	{
		CustomSearch.Cx		= customSearchKey.Cx;
		CustomSearch.ApiKey	= customSearchKey.ApiKey;
	}

	private static void CreateService()
	{ 
		_service = new CustomSearchAPIService(
			new BaseClientService.Initializer
			{
				ApplicationName	= "Custom Search",
				ApiKey			= _apiKey,
			}
		);
	}

	/// <summary>
	/// Perform a Google search.
	/// </summary>
	/// <param name="query">Search string.</param>
	/// <param name="start">List request start.</param>
	public static IList<Result> Search(string query, int start = 0)
	{
		CseResource.ListRequest listRequest = NewListRequest(query, start);
		return  listRequest.Execute().Items;
	}

	/// <summary>
	/// Perform a Google search.
	/// </summary>
	/// <param name="query">Search string.</param>
	/// <param name="site">Site to search.</param>
	/// <param name="start">List request start.</param>
	public static IList<Result> SiteSearch(string query, string site, int start = 0)
	{
		CseResource.ListRequest listRequest	= NewListRequest(query, start);
		listRequest.SiteSearch				= site;
		listRequest.SiteSearchFilter		= CseResource.ListRequest.SiteSearchFilterEnum.I;
		return listRequest.Execute().Items;
	}

	/// <summary>
	/// Perform a Google search.
	/// </summary>
	/// <param name="query">Search string.</param>
	/// <param name="start">List request start.</param>
	public static CseResource.ListRequest NewListRequest(string query, int start = 0)
	{
		CseResource.ListRequest listRequest	= CustomSearch.Service.Cse.List();
		listRequest.Cx						= _cx;
		listRequest.Q						= query;
		listRequest.Start					= start;
		return listRequest;
	}

	#endregion

} // End class.