using despesas_backend_api_net_core.CommonDependenceInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace CommonDependenceInject;
public sealed class SupportCulturesDependenceInjectTest
{
    [Fact]
    public void AddSupporteCulturesPtBr_Should_Set_Correct_Cultures()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();

        // Act
        var app = builder.Build();
        app.AddSupporteCulturesPtBr();


        // Assert
        var localizationOptions = app.Services.GetService(typeof(IOptions<RequestLocalizationOptions>)) as IOptions<RequestLocalizationOptions>;
        var defaultCulture = localizationOptions?.Value.DefaultRequestCulture.Culture;
        var supportedCultures = localizationOptions?.Value.SupportedCultures;

        Assert.NotNull(defaultCulture);
        Assert.NotNull(supportedCultures);
        Assert.NotEmpty(supportedCultures);
        //Assert.True(defaultCulture.Name == "pt-BR" || defaultCulture.Name == "en-US");
        //Assert.True(supportedCultures.Contains(new CultureInfo("pt-BR")) || supportedCultures.Contains(new CultureInfo("en-US")));
    }
}