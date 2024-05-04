using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace despesas_backend_api_net_core.CommonDependenceInject;
public static class SupportCulturesDependenceInject
{
    public static void AddSupporteCulturesPtBr(this WebApplication app)
    {
        var supportedCultures = new[] { new CultureInfo("pt-BR") };
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        };
        app.UseRequestLocalization(localizationOptions);
    }
}