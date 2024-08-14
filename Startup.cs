using System;
using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

[assembly: FunctionsStartup(typeof(Company.Function.Startup))]

namespace Company.Function;

public class Startup : FunctionsStartup
{
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        base.ConfigureAppConfiguration(builder);

        var settings = builder.ConfigurationBuilder.Build();
        builder.ConfigurationBuilder.AddAzureAppConfiguration(options =>
            options
                .Connect(new Uri(settings["AppConfigEndpoint"]), new DefaultAzureCredential())
                .Select(KeyFilter.Any, "dev"));
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {}
}
