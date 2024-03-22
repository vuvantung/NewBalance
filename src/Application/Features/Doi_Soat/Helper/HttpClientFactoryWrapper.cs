using System.Net.Http;

namespace EMS.Internal.BlazorWeb.Helper
{
	public class HttpClientFactoryWrapper
	{
		private readonly HttpClient _httpClient;

		public HttpClientFactoryWrapper(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("BlazorWASMHttpClient");
		}

		public HttpClient GetClient()
		{
			return _httpClient;
		}
	}
}
