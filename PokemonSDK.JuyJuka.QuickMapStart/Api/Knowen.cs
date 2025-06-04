namespace PokemonSDK.JuyJuka.QuickMapStart.Api
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled;

  public class Knowen
  {
    public static readonly string MusicGrass = "route 30.ogg";
    public static readonly string MusicUraban = "43 camphrier town.ogg";

    public static readonly object Nothing = 0;
    public static readonly object Grass = 1 + 0;
    public static readonly object Water = 1 + 150;
    public static readonly object Mointain = 1 + 689;
    public static readonly object RoughTerrainSandy = 1 + 92;
    public static readonly object RoughTerrainRocky = 1 + 710;
    public static readonly object Uraban = 1 + 238;

    public static readonly object SystemTagSea = 4257 + 21;
    public static readonly object SystemTagGrass = 4257 + 5;
    public static readonly object SystemTagSand = 4257 + 14;
    public static readonly object SystemTagClimb = 4257 + 62;


    public static readonly object PassagesX = 4385 + 15;

    public static readonly object PanelGrass = 1;
    public static readonly object PanelForest = 6;
    public static readonly object PanelSea = 8;
    public static readonly object PanelMointain = 4;
    public static readonly object PanelRoughTerrain = 5;
    public static readonly object PanelUrban = 3;

    public static readonly SingleShape Flower1 = new SingleShape(false, 1 + 1);
    public static readonly SingleShape Flower2 = new SingleShape(false, 1 + 3);
    public static readonly SingleShape Bolder = new SingleShape(true, 1 + 626);
    public static readonly SingleShape Rubble = new SingleShape(false, 1 + 4178);
    public static readonly SingleShape SandGreenery = new SingleShape(false, 1 + 3259);

    /*
    N NE NW
    S SE SW
    E
    W
     */
    public static readonly Knowen.Border BorderSea = new Knowen.Border(
      1 + 148, 1 + 142, 1 + 143,
      1 + 132, 1 + 134, 1 + 135,
      1 + 139,
      1 + 141);
    public static readonly Knowen.Border BorderUrban = new Knowen.Border(
      1 + 230, 1 + 231, 1 + 229,
      1 + 246, 1 + 247, 1 + 245,
      1 + 239,
      1 + 237);
    /*
    public static readonly Knowen.Border BorderMountain = new Knowen.Border(
      1 + 681, 1 + 682, 1 + 680,
      1 + 697, 1 + 698, 1 + 696,
      1 + 690,
      1 + 688);
    */

    public static readonly Tuple<Tuple<string, Knowen.Border>, Tuple<string, Knowen.Border>, Tuple<string, Knowen.Border>> BorderMountain = new Tuple<Tuple<string, Border>, Tuple<string, Border>, Tuple<string, Border>>(
      new Tuple<string, Border>(TmxMapExportFormat.Layer1.Item2, new Knowen.Border(
      1 + 681, 1 + 682, 1 + 680,
      1 + 697, 1 + 698, 1 + 696,
      1 + 690,
      1 + 688)),
      new Tuple<string, Border>(TmxMapExportFormat.LayerS, new Knowen.Border(
        Knowen.SystemTagClimb, Knowen.Nothing, Knowen.Nothing,
        Knowen.SystemTagClimb, Knowen.Nothing, Knowen.Nothing,
        Knowen.SystemTagClimb,
        Knowen.SystemTagClimb)),
      new Tuple<string, Border>(TmxMapExportFormat.LayerP, new Knowen.Border(
        Knowen.PassagesX, Knowen.PassagesX, Knowen.PassagesX,
        Knowen.PassagesX, Knowen.PassagesX, Knowen.PassagesX,
        Knowen.PassagesX,
        Knowen.PassagesX))
      );

    public class Border
    {
      public Border(params object[] objects)
      {
        int i = Map._0;
        if (objects != null && objects.Length > i) this.North = objects[i++];
        if (objects != null && objects.Length > i) this.NorthEast = objects[i++];
        if (objects != null && objects.Length > i) this.NorthWest = objects[i++];
        if (objects != null && objects.Length > i) this.South = objects[i++];
        if (objects != null && objects.Length > i) this.SouthEast = objects[i++];
        if (objects != null && objects.Length > i) this.SouthWest = objects[i++];
        if (objects != null && objects.Length > i) this.East = objects[i++];
        if (objects != null && objects.Length > i) this.West = objects[i++];
      }

      public readonly object? North = Knowen.Nothing;
      public readonly object? NorthEast = Knowen.Nothing;
      public readonly object? NorthWest = Knowen.Nothing;
      public readonly object? South = Knowen.Nothing;
      public readonly object? SouthEast = Knowen.Nothing;
      public readonly object? SouthWest = Knowen.Nothing;
      public readonly object? East = Knowen.Nothing;
      public readonly object? West = Knowen.Nothing;
    }
  }
}