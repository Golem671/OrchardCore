using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Admin.Drivers;
using OrchardCore.Navigation;

namespace OrchardCore.Admin
{
    // Builds admin menu with given localizer
    public class AdminMenu : INavigationProvider
    {
        // Constructor, set string localizer type
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }

        public IStringLocalizer T { get; }

        // Asynchronous navigation build
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            // Check for admin
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            // Build navigation structure
            builder
                .Add(T["Configuration"], design => design
                    .Add(T["Settings"], settings => settings
                        .Add(T["Admin"], T["Admin"], zones => zones
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = AdminSiteSettingsDisplayDriver.GroupId })
                            .Permission(PermissionsAdminSettings.ManageAdminSettings)
                            .LocalNav()
                        )
                    ));

            return Task.CompletedTask;
        }
    }
}
