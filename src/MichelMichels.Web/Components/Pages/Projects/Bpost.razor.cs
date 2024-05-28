using MichelMichels.BpostSharp.Models;
using MichelMichels.BpostSharp.Services;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace MichelMichels.Web.Components.Pages.Projects;

public partial class Bpost : ComponentBase
{
    private string _postalCodeInput = string.Empty;

    [Inject]
    protected BelgianCityDataService BelgianCityDataService { get; set; } = default!;

    public string PostalCodeInput { get; set; } = string.Empty;
    public List<CityData> SearchResults { get; set; } = [];

    private async Task Search()
    {
        List<CityData> data = await BelgianCityDataService.GetByPostalCode(PostalCodeInput);
        SearchResults.Clear();
        SearchResults.AddRange(data);

        Debug.WriteLine($"Postal code changed to '{PostalCodeInput}'");
    }
}
