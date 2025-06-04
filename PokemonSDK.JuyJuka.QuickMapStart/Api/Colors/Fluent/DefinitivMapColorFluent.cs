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

    public DefinitivMapColorFluent AddShapeRandomly<Shape>(int x, int y, int fx, int fy, params Action<Shape>[] action)
      where Shape : IShape, new()
    {
      return this.AddShapeRandomly(x, y, fx, fy, new Shape(), s => this.DoActions((Shape)s, action));
    }

    public DefinitivMapColorFluent AddShapeRandomly(int x, int y, int fx, int fy, IShape shape, params Action<IShape>[] action)
    {
      int maxY = Map._0;
      int maxX = Map._0;
      Shapes.Shape.Max((shape as Shapes.Shape)?.Points, out maxX, out maxY);
      return this.AddShape(x, y, shape, new SeamingRandomShapePositon()
      {
        WorldMapCoordinatesXModulo = x,
        WorldMapCoordinatesYModulo = y,
        FrequencyX = Math.Max(fx, Map._0) + Math.Abs(maxX)+1,
        FrequencyY = Math.Max(fy, Map._0) + Math.Abs(maxY)+1,
      }, action);
    }

    public DefinitivMapColorFluent AddShapeAt<Shape>(int x, int y, params Action<Shape>[] action)
      where Shape : IShape, new()
    {
      return this.AddShapeAt(x, y, new Shape(), s => this.DoActions((Shape)s, action));
    }

    public DefinitivMapColorFluent AddShapeAt(int x, int y, IShape shape, params Action<IShape>[] action)
    {
      return this.AddShape(x, y, shape, new AtShapePositon(x, y), action);
    }

    private DefinitivMapColorFluent AddShape(int x, int y, IShape shape, IShapePositon pos, params Action<IShape>[] action)
    {
      if (shape != null)
      {
        shape.Position = pos;
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