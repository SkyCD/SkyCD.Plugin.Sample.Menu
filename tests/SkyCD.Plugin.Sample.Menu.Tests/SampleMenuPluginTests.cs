using System.Threading.Tasks;
using SkyCD.SDK.Capabilities.Menu;
using SkyCD.Testing;
using Xunit;

namespace SkyCD.Plugin.Sample.Menu.Tests;

public sealed class SampleMenuPluginTests
{
    [Fact]
    public void AssemblyDeclaresMenuCapability()
    {
        var metadata = PluginAssemblyMetadata.Inspect(typeof(SampleMenuPlugin).Assembly);

        Assert.Equal("SkyCD.Plugin.Sample.Menu", metadata.Id);
        Assert.Contains(typeof(SampleMenuPlugin), metadata.CapabilityTypes);
    }

    [Fact]
    public void ContributesExampleCommandToToolsMenu()
    {
        var contribution = Assert.Single(new SampleMenuPlugin().GetMenuContributions());

        Assert.Equal("sample.menu.example", contribution.CommandId);
        Assert.Equal("Example", contribution.Title);
        Assert.Equal("Tools", contribution.Location);
        Assert.Equal(100, contribution.Order);
    }

    [Fact]
    public async Task UnknownCommandCompletesWithoutOpeningWindow()
    {
        var plugin = new SampleMenuPlugin();

        await plugin.ExecuteMenuCommandAsync("unknown.command", new MenuCommandContext());
    }
}
