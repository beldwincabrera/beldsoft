using MudBlazor;

namespace Beldsoft.Web;

public static class BeldsoftTheme
{
    public static MudTheme Create() => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#000DFF",
            Secondary = "#C115EC",
            Background = "#FFFFFF",
            Surface = "#F2F2F2",
            AppbarBackground = "#FFFFFF",
            DrawerBackground = "#000000",
            TextPrimary = "#000000",
            TextSecondary = "#666666",
            ActionDefault = "#000DFF",
            Error = "#FF3B30",
            Success = "#34C759",
            Warning = "#FF9500",
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Manrope", "sans-serif"],
                FontSize = "15px",
                LineHeight = "1.7",
            },
            H1 = new H1Typography { FontFamily = ["Outfit", "sans-serif"], FontWeight = "700" },
            H2 = new H2Typography { FontFamily = ["Outfit", "sans-serif"], FontWeight = "700" },
            H3 = new H3Typography { FontFamily = ["Outfit", "sans-serif"], FontWeight = "600" },
            H4 = new H4Typography { FontFamily = ["Outfit", "sans-serif"], FontWeight = "600" },
            H5 = new H5Typography { FontFamily = ["Outfit", "sans-serif"], FontWeight = "600" },
            H6 = new H6Typography { FontFamily = ["Outfit", "sans-serif"], FontWeight = "600" },
        },
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "8px",
        }
    };
}
