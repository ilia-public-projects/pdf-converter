using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfConverterFunction;
using PdfConverterFunction.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace PdfConverterFunction
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            //FunctionsHostBuilderContext context = builder.GetContext();
            //builder.ConfigurationBuilder.AddJsonFile(Path.Combine(context.ApplicationRootPath, "settings.json"), optional: false, reloadOnChange: true).AddEnvironmentVariables().Build();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = builder.GetContext().Configuration;


            builder.Services.AddLogging();
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IHeaderService, HeaderService>();
            builder.Services.AddSingleton<IPdfConverter, PdfConverter>();


            //builder.Services.AddOptions<SettingsModel>().Configure<IConfiguration>((settings, configuration) =>
            //{
            //    configuration.GetSection("Configuration").Bind(settings);
            //});


        }


    }
}
