namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors
{
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;

  public sealed partial class DefinitivMapColorFluent
  {
    public static DefinitivMapColorFluent New(string name, Color color, object background)
    {
      return new DefinitivMapColorFluent()
      {
        DefinitivMapColor = new DefinitivMapColor(name, color, background)
      };
    }

    public DefinitivMapColor DefinitivMapColor { get; private set; } = new DefinitivMapColor(string.Empty, Color.Transparent);

    public DefinitivMapColorFluent Set(System.Action<DefinitivMapColor> action)
    {
      if (action != null) action(this.DefinitivMapColor);
      return this;
    }

    public DefinitivMapColorFluent Panel(object panel)
    {
      string? tag = string.Empty + panel;
      if (string.IsNullOrWhiteSpace(tag)) tag = null;
      this.DefinitivMapColor.Panel = tag ?? string.Empty + Knowen.Nothing;
      return this;
    }

    public DefinitivMapColorFluent Music(string name)
    {
      this.DefinitivMapColor.MusicName = name;
      return this;
    }
  }
}