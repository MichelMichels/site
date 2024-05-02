using MichelMichels.DobissSharp.Api;
using MichelMichels.DobissSharp.Api.Models;
using Microsoft.AspNetCore.Components;

namespace MichelMichels.Web.Components.Pages.Projects;

public partial class Dobiss : ComponentBase
{
    private DobissClient? dobissClient;

    public string BaseUrl { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public bool IsLoggedIn => dobissClient is not null;
    public bool IsBusy { get; set; }
    public DiscoverResponse? LastDiscoverResponse { get; set; }

    public async Task Login()
    {
        IsBusy = true;

        DobissClientOptions options = new()
        {
            BaseUrl = this.BaseUrl,
            SecretKey = this.SecretKey,
        };
        dobissClient = new DobissClient(options);

        try
        {
            await Discover();
        }
        catch
        {
            dobissClient = null;
        }

        IsBusy = false;
    }

    public async Task Discover()
    {
        if (dobissClient is null)
        {
            throw new NullReferenceException();
        }

        LastDiscoverResponse = await dobissClient.Discover();
    }
}
