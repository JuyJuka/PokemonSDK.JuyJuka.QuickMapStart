namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled;

  using X = PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.DefinitivMapColor.FunctionParameters;
  using Y = Tuple<string, Point, string>;
  public class Shape : IShape
  {
    public virtual IShapePositon? Position { get; set; } = null;
    public virtual Y[] Points { get; set; } = [];

    public static string[] DefaultLayers { get { return TmxMapExportFormat.Layers; } }
    public static string[] SensibleLayers
    {
      get
      {
        return [
      TmxMapExportFormat.LayerS,
      TmxMapExportFormat.LayerP,
      TmxMapExportFormat.Layer2.Item2,
      TmxMapExportFormat.Layer2.Item1,
      TmxMapExportFormat.Layer1.Item2,
      ];
      }
    }
    protected static string X { get { return string.Empty + Knowen.PassagesX; } }
    protected static string _ { get { return string.Empty; } }

    public Shape() { }
    public Shape(string nothing, object[,] systemtags, object[,] passages, object[,] layer22, object[,] layer21, object[,] layer12)
      : this(SensibleLayers, systemtags, passages, layer22, layer21, layer12) { }
    public Shape(string[] layers, params object?[][,] points)
    {
      List<Y> list = new List<Y>();
      if (layers != null)
      {
        for (int i = Map._0; i < layers.Length; i++)
        {
          if (points.Length < i) continue;
          if (points[i] == null) continue;
          string layer = layers[i];
          int maxX = points[i].GetLength(Map._0);
          int maxY = points[i].GetLength(Map._1);
          for (int x = Map._0; x < maxX; x++)
            for (int y = Map._0; y < maxY; y++)
            {
              list.Add(new Y(layer, new Point(y,x), string.Empty + points[i][x, y]));
            }
        }
      }
      this.Points = list.ToArray();
    }

    public string ToLayer(X parameters)
    {
      Point original = parameters.Point;
      int sizeMaxY = parameters.Map.World.Size.Height;
      int sizeMaxX = parameters.Map.World.Size.Width;
      int sizeMinY = Map._0;
      int sizeMinX = Map._0;
      int maxY = Map._0;
      int maxX = Map._0;

      foreach (var x in this.Points)
      {
        maxY = Math.Max(maxY, x.Item2.Y);
        maxX = Math.Max(maxX, x.Item2.X);
      }

      Point shapeStart = original;
      bool shapeStart_IsStart = false;
      for (int y = 0; y <= maxY; y++)
      {
        for (int x = 0; x <= maxX; x++)
        {
          shapeStart = new Point(original.X - x, original.Y - y);
          if (this.IsStart(parameters, shapeStart)) shapeStart_IsStart = true;
          if (shapeStart_IsStart) break;
        }
        if (shapeStart_IsStart) break;
      }
      if (!shapeStart_IsStart) return string.Empty;
      if (shapeStart.X < sizeMinX || shapeStart.Y < sizeMinX) return string.Empty;
      if (shapeStart.X + maxX >= sizeMaxX || shapeStart.Y + maxY >= sizeMaxY) return string.Empty;
      Point pointInShape = new Point(original.X - shapeStart.X, original.Y - shapeStart.Y);

      foreach (Y point in this.Points ?? [])
        if (point.Item1 == parameters.LayerName && pointInShape.Y == point.Item2.Y && pointInShape.X == point.Item2.X)
          return point.Item3;
      return string.Empty;
    }

    protected virtual bool IsStart(X parameters, Point start)
    {
      if (this.Position != null) return this.Position.IsStart(parameters, start);
      return start.X == Map._0 && start.Y == Map._0;
    }
  }


  public class TestShape : Shape
  {
    public TestShape() : base(string.Empty
      , new object[,] {
        { _, _ },
        { _, _ },
      }
      , new object[,] {
        { 4385 + 10, 4385 + 12 },
        { 4385 + 03, 4385 + 05 },
      }
      , new object[,] {
        { _, _ },
        { _, _ },
      }
      , new object[,] {
        { _, _ },
        { _, _ },
      }
      , new object[,] {
        { _, _ },
        { _, _ },
      }
      )
    { }
  }
}
