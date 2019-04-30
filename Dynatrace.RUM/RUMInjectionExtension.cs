using Microsoft.AspNetCore.Builder;
using MM.Core.IO;
using MM.Middleware;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class RUMInjectionExtension
    {
        private static async Task<string> GetJavascript(string apiEndpoint, string apiToken, string applicationId, bool inlineJavascript)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Api-Token " + apiToken);

                HttpResponseMessage response = await client.GetAsync(apiEndpoint+(inlineJavascript? "/api/v1/rum/jsInlineScript/" : "/api/v1/rum/jsTag/") + applicationId);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }

            return null;
        }
        public static IApplicationBuilder UseDynatraceRUM(this IApplicationBuilder builder, string apiEndpoint, string apiToken, string applicationId, bool inlineJavascript = false)
        {
            return builder.UseDynatraceRUM(apiEndpoint,
                                            apiToken,
                                            applicationId,
                                            inlineJavascript,
                                            new string[] { "<script ", "</head>"}, 
                                            InsertType.Before, 
                                            new string[] { "text/html", "application/xhtml+xml", "application/xhtml+xml" });
        }

        public static IApplicationBuilder UseDynatraceRUM(this IApplicationBuilder builder, string apiEndpoint, string apiToken, string applicationId, bool inlineJavascript , string[] searchTags, InsertType insertType, string[] filterContentTypes  )
        {
            var insert = GetJavascript(apiEndpoint, apiToken, applicationId, inlineJavascript).Result; 

            if (!string.IsNullOrEmpty(insert))
            {
                return builder.UseMiddleware<HtmlInjectionMiddleware>(filterContentTypes,
                                                                        searchTags,
                                                                        insertType,
                                                                        insert);
            }
            else
                return builder;
        }
    }
}

