namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors
{
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes;

  public sealed partial class DefinitivMapColorFluent
  {
    public static DefinitivMapColorFluent New(string name, Color color, object background)
    {
      return new DefinitivMapColorFluent()
      {
        DefinitivMapColor = new DefinitivMapColor(name, color, background)
      };
    }

    private void DoActions<T>(T obj, params Action<T>[] action)
    {
      if (action != null) foreach (Action<T> action_ in action) if (action_ != null) action_(obj);
    }

    public DefinitivMapColorFluent AddShapeAt<Shape>(int x, int y, params Action<Shape>[] action)
      where Shape : IShape, new()
    {
      return this.AddShapeAt(x, y, new Shape(), s => this.DoActions((Shape)s, action));
    }

    public DefinitivMapColorFluent AddShapeAt(int x, int y, IShape shape, params Action<IShape>[] action)
    {
      if (shape != null)
      {
        shape.Position = new AtShapePositon(x, y);
        this.DoActions(shape, action);
        this.DefinitivMapColor._Functions.Insert(Map._0, shape.ToLayer);
      }
      return this;
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