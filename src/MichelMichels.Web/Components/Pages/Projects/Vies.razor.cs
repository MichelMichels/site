using MichelMichels.ViesSharp;
using MichelMichels.ViesSharp.Exceptions;
using MichelMichels.ViesSharp.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace MichelMichels.Web.Components.Pages.Projects;

public partial class Vies : ComponentBase
{
    [Inject]
    protected IViesSharpClient ViesSharpClient { get; set; } = default!;

    private const string nugetUrl = "https://www.nuget.org/packages/MichelMichels.ViesSharp/";
    private const string githubUrl = $"{Constants.GitHubProfileUrl}/ViesSharp";
    private const string projectName = "ViesSharp";

    private readonly JsonSerializerOptions defaultSerializerOptions = new()
    {
        WriteIndented = true,
    };

    public string VatNumber { get; set; } = string.Empty;
    public string Json { get; set; } = string.Empty;
    public string SelectedCountryCode { get; set; } = string.Empty;
    public bool IsLookupEnabled => !string.IsNullOrEmpty(SelectedCountryCode);
    public List<string> CountryCodes = [];
    public VatNumberResponse? LastResponse { get; set; }
    public List<VatNumberResponse> History = [];
    public string BackgroundClass { get; set; } = string.Empty;
    public bool IsLookingUp { get; set; }
    public string ParsedCompanyName { get; set; } = string.Empty;
    public string ParsedAddress { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        StatusResponse statusResponse = await ViesSharpClient.CheckStatus();
        CountryCodes = statusResponse.Countries.Select(x => x.CountryCode).ToList();
    }

    public async Task Lookup()
    {
        if (IsLookingUp)
        {
            return;
        }

        IsLookingUp = true;

        VatNumberRequest request = new()
        {
            CountryCode = SelectedCountryCode,
            VatNumber = ExtractDigits(VatNumber),
        };

        try
        {
            if (LastResponse is not null)
            {
                History.Add(LastResponse);
            }

            LastResponse = await ViesSharpClient.CheckVatNumber(request);
            ParseResponse(LastResponse);

            BackgroundClass = LastResponse.IsValid ? "bg-success" : "bg-danger";
            Json = JsonSerializer.Serialize(LastResponse, defaultSerializerOptions);
        }
        catch (ViesSharpException ex)
        {
            Json = JsonSerializer.Serialize(ex.ErrorResponse, defaultSerializerOptions);
        }

        IsLookingUp = false;
    }

    private void ParseResponse(VatNumberResponse response)
    {
        ParsedCompanyName = response.Name;
        ParsedAddress = response.Address.Replace("\n", ", ");
    }
    private static string ExtractDigits(string value)
    {
        return string.Join("", value.Where(x => char.IsDigit(x)));
    }
}
