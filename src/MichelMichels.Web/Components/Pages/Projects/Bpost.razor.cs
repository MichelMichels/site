using MichelMichels.BpostSharp.Models;
using MichelMichels.BpostSharp.Services;
using Microsoft.AspNetCore.Components;
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

    public bool IsSearching { get; set; }
    public List<CityData> SearchResults { get; set; } = [];
    public int MunicipalityCount => SearchResults.Where(x => x.IsMunicipality ?? false).Count();
    public int CityCount => SearchResults.Where(x => !x.IsMunicipality ?? false).Count();


    protected override async Task OnParametersSetAsync()
    {
        PostalCodeInput = PostalCodeInput ?? string.Empty;

        await Search();
    }

    private async Task Search()
    {
        await Task.CompletedTask;

        SearchResults.Clear();
        if (PostalCodeInput!.Length < 1)
        {
            return;
        }

        IsSearching = true;

        List<CityData> data = await BelgianCityDataService.GetByPostalCode(PostalCodeInput);
        SearchResults.AddRange(data);

        IsSearching = false;
    }
}
