using MichelMichels.Web.Components.Models;
using Microsoft.AspNetCore.Components;

namespace MichelMichels.Web.Components.Pages.Projects;

public partial class Projects : ComponentBase
{
    public List<ProjectThumbnail> Cards { get; set; } = [
        new ProjectThumbnail {
            Emoji = "🛂",
            Title = "ViesSharp",
            Description = "C# API client to validate VAT numbers of European member states through VOW.",
            Link = "/projects/vies"
        },
        new ProjectThumbnail {
            Emoji = "📯",
            Title = "BpostSharp",
            Description = "C# service to obtain city data provided by the Belgian postal service.",
            Link = "http://git.miche.ls/BpostSharp"
        }
    ];
}
