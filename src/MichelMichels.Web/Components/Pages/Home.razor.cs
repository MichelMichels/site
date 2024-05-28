using MichelMichels.Web.Components.Models;
using Microsoft.AspNetCore.Components;

namespace MichelMichels.Web.Components.Pages;

public partial class Home : ComponentBase
{
    private Random random = new();

    public List<ContactData> ContactOptions { get; set; } = [];

    public string Name { get; set; } = "Michel Michels";

    protected override void OnInitialized()
    {
        ContactOptions = [
            new ContactData {
                Link = "mailto:michel@miche.ls",
                IconClass = "bi-envelope-at",
            },
            new ContactData {
                Link = "https://www.linkedin.com/in/michelmichelsjr/",
                IconClass = "bi-linkedin",
            },
            new ContactData {
                Link = "https://github.com/MichelMichels/",
                IconClass = "bi-github",
            }
        ];
    }

    private string GetRandomColorClass()
    {
        return $"c-4-{random.Next(16)}";
    }
}
