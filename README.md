# Dynatrace.RUM 
[![NuGet](http://img.shields.io/nuget/v/Dynatrace.RUM.svg)](https://www.nuget.org/packages/Dynatrace.RUM/)

The Dynatrace.RUM middleware for ASP.NET Core automatically injects a javascript tag required for [Dynatrace Real User Monitoring](https://www.dynatrace.com/support/help/how-to-use-dynatrace/real-user-monitoring/) into your HTML output. 

The extension utilizes the [Dynatrace REST-API](https://www.dynatrace.com/support/help/extend-dynatrace/dynatrace-api/environment/rum-and-javascript-api/) to get latest javascript code. 

## How-To-Use
Like any other middleware, configuration has to be done in the `Startup.Configure` method. Providing an AppBuilder extension you smply need to add the `app.UseDynatraceRUM(...)`

### Prerequisites
- Generate a [Dynatrace API Token](https://www.dynatrace.com/support/help/shortlink/api-authentication#generate-a-token)
- Get your [Environment-ID](https://www.dynatrace.com/support/help/get-started/introduction/why-do-i-need-an-environment-id/)
- Prepare your API-Endpoint, which is either `https://{your-domain}/e/{your-environment-id}` for Dynatrace Managed or `https://{your-environment-id}.live.dynatrace.com` for Dynatrace SaaS
- Setup [agentless monitoring](https://www.dynatrace.com/support/help/shortlink/agentless-rum#set-up-agentless-monitoring)
- Get your application-id either [via REST-API](https://www.dynatrace.com/support/help/shortlink/api-javascript#anchor_manual-app) or you can click applications currently set up for agentless monitoring. Then you will view the list of currently set up applications where you retrieve the application-id from the brower url if you browse to your application of interest. 


### Example
``` 
public void Configure(IApplicationBuilder app)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
    }

    //Enable automatic injection of Dynatrace RUM Javascript tag, using an inline script.
    app.UseDynatraceRUM("<Dynatrace-API-Endpoint>", "<Dynatrace-API-Token>", "<Your-Dynatrace-Application-Id>", true)

    app.UseStaticFiles();
    app.UseMvc();
}
```

## Where can I get it?
This package is available on [nuget.org](https://nuget.org)

## License
This package is licensed under the [Apache 2.0 license](LICENSE.txt).
