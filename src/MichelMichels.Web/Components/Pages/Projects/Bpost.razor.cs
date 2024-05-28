using MichelMichels.BpostSharp.Models;
using MichelMichels.BpostSharp.Services;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Globalization;

namespace MichelMichels.Web.Components.Pages.Projects;

public partial class Bpost : ComponentBase
{
    private readonly TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
    private const string nugetUrl = "https://www.nuget.org/packages/MichelMichels.BpostSharp/";

    [Inject]
    protected BelgianCityDataService BelgianCityDataService { get; set; } = default!;

    [Parameter]
    public string? PostalCodeInput { get; set; }
    public List<CityData> SearchResults { get; set; } = [];

    protected override async Task OnParametersSetAsync()
    {
        PostalCodeInput = PostalCodeInput ?? string.Empty;

        await Search();
    }

    private async Task Search()
    {
        SearchResults.Clear();
        if (PostalCodeInput!.Length < 2)
        {
            return;
        }

        List<CityData> data = await BelgianCityDataService.GetByPostalCode(PostalCodeInput);
        SearchResults.AddRange(data);

        Debug.WriteLine($"Postal code changed to '{PostalCodeInput}'");
    }
}
