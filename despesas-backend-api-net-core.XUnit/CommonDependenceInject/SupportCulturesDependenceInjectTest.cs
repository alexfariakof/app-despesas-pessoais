using despesas_backend_api_net_core.CommonDependenceInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace CommonDependenceInject;
public class SupportCulturesDependenceInjectTest
{
    [Fact]
    public async Task AddSupporteCulturesPtBr_Should_Set_Correct_Cultures()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();

        // Act
        var app = builder.Build();
        app.AddSupporteCulturesPtBr();


        // Assert
        var localizationOptions = app.Services.GetService(typeof(IOptions<RequestLocalizationOptions>)) as IOptions<RequestLocalizationOptions>;
        var defaultCulture = localizationOptions.Value.DefaultRequestCulture.Culture;
        var supportedCultures = localizationOptions.Value.SupportedCultures;

        Assert.True(defaultCulture.Name == "pt-BR" || defaultCulture.Name == "en-US");
        Assert.True(supportedCultures.Contains(new CultureInfo("pt-BR")) || supportedCultures.Contains(new CultureInfo("en-US")));
    }
}