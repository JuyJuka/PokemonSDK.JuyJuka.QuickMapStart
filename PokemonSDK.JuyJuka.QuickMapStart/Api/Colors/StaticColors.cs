namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Fluent;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled;

  public static class StaticColors
  {
    #region Source Link Doku
    /*
     https://bulbapedia.bulbagarden.net/wiki/List_of_Pok%C3%A9mon_by_habitat
1.1	Grassland Pokémon
1.2	Forest Pokémon
1.3	Water's-edge Pokémon
1.4	Sea Pokémon
1.5	Cave Pokémon
1.6	Mountain Pokémon
1.7	Rough-terrain Pokémon
1.8	Urban Pokémon
1.9	Rare Pokémon
     */
    #endregion Source Link Doku

    #region Grassland
    public static DefinitivMapColor Grassland =
      FluentNew.NewColor(nameof(Grassland))
      .WithUserColor(Color.Green).From(78, 122)
      .ToBackground(Knowen.Grass)

      .WithMusic(Knowen.MusicGrass)
      .WithMapNamePanel(Knowen.PanelGrass)
      .WithShape<GrassPatchShape>().SpreadRandomly(13, 7, 3, 3).Added()
      .WithShape<GrassPatchShape>().SpreadRandomly(8, 4, 0, 0).Added()
      .WithShape(Knowen.Flower1).SpreadRandomly(13, 7, 0, 0).Added()
      .WithShape(Knowen.Flower2).SpreadRandomly(8, 1, 0, 0).Added()
      .WithShape<BushShape>().SpreadRandomly(13, 7, 3, 3).Added()

      .AllAroundTheWorld()
      ;
    #endregion Grassland

    #region Forest
    public static DefinitivMapColor Forest =
      FluentNew.NewColor(nameof(Forest))
      .WithUserColor(Color.DarkGreen).From(123, 143)
      .ToBackground(Knowen.Grass)
      .WithMapNamePanel(Knowen.PanelForest)
      .WithMusic(Knowen.MusicGrass)
      .WithShape<TreeShape>().SpreadRandomly(13, 7, 0, 0).Added()
      .WithShape<TreeShape>().SpreadRandomly(8, 4, 0, 0).Added()
      .WithShape<TreeShape>().SpreadRandomly(5, 1, 0, 0).Added()
      .WithShape<GrassPatchShape>().SpreadRandomly(13, 7, 3, 3).Added()
      .WithShape<GrassPatchShape>().SpreadRandomly(8, 4, 0, 0).Added()
      .WithShape<StickPille1Shape>().SpreadRandomly(4, 10, 5, 7).Added()
      .WithShape<StickPille2Shape>().SpreadRandomly(5, 9, 10, 3).Added()
      .WithShape<StickPille3Shape>().SpreadRandomly(5, 7, 5, 9).Added()
      .AllAroundTheWorld()
      ;

    public static DefinitivMapColor Forest_Sothern =
      FluentNew.NewColor(nameof(Forest_Sothern))
      .WithUserColor(Color.GreenYellow).From(123, 143)
      .ToBackground(Knowen.Grass)
      .WithMapNamePanel(Knowen.PanelForest)
      .WithMusic(Knowen.MusicGrass)
      .WithShape<TreeShape>().SpreadRandomly(10, 6, 0, 0).Added()
      .WithShape<PalmTreeShape>().SpreadRandomly(4, 1, 0, 0).Added()
      .WithShape<GrassPatchShape>().SpreadRandomly(13, 7, 3, 3).Added()
      .WithShape<StickPille2Shape>().SpreadRandomly(5, 9, 10, 3).Added()
      .WithShape<StickPille3Shape>().SpreadRandomly(5, 7, 5, 9).Added()
      .WithShape<GrassPatchShape>().SpreadRandomly(8, 4, 0, 0).Added()
      .WithShape<PalmTreeShape>().SpreadRandomly(13, 7, 2, 1).Added()
      .WithShape<StickPille1Shape>().SpreadRandomly(4, 10, 5, 7).Added()
      .LimitedTo().Southern(30).Percent()
      ;
    #endregion Forest

    #region Sea
    public static DefinitivMapColor Sea =
      FluentNew.NewColor(nameof(Sea))
      .WithUserColor(Color.Blue).From(144, 249)
      .ToBackground(Knowen.Water)
      .DefaultSystemTagSea()
      .WithMapNamePanel(Knowen.PanelSea)
      .WithBorder(Knowen.BorderSea)
      .WithMusic(Knowen.MusicGrass)
      .AllAroundTheWorld()
      ;
    #endregion Sea

    #region Mountain
    public static DefinitivMapColor Mountain =
      FluentNew.NewColor(nameof(Mountain))
      .WithUserColor(Color.DarkGray).From(0, 21)
      .ToBackground(Knowen.Mointain)
      .WithBorder(Knowen.BorderMountain.Item1.Item1, Knowen.BorderMountain.Item1.Item2)
      .WithBorder(Knowen.BorderMountain.Item2.Item1, Knowen.BorderMountain.Item2.Item2)
      .WithBorder(Knowen.BorderMountain.Item3.Item1, Knowen.BorderMountain.Item3.Item2)
      .AllAroundTheWorld()
      ;

    public static DefinitivMapColor Mountain_Snowy =
      FluentNew.NewColor(nameof(Mountain_Snowy))
      .WithUserColor(Color.WhiteSmoke).From(0, 21)
      .ToBackground(Knowen.Mointain)
      .WithBorder(Knowen.BorderMountain.Item1.Item1, Knowen.BorderMountain.Item1.Item2)
      .WithBorder(Knowen.BorderMountain.Item2.Item1, Knowen.BorderMountain.Item2.Item2)
      .WithBorder(Knowen.BorderMountain.Item3.Item1, Knowen.BorderMountain.Item3.Item2)
      .WithShape<SnowPatch3x3Shape>().SpreadRandomly(13, 7, 3, 3).Added()
      .WithShape<SnowPatch3x3Shape>().SpreadRandomly(8, 4, 0, 0).Added()
      .WithShape<SnowPatch2x3Shape>().SpreadRandomly(5, 9, 10, 3).Added()
      .WithShape<SnowPatch2x2Shape>().SpreadRandomly(9, 7, 5, 9).Added()
      .LimitedTo().Northern(30).Percent()
      ;
    #endregion Mountain

    #region RoughTerrain
    public static DefinitivMapColor RoughTerrain_Sandy =
      FluentNew.NewColor(nameof(RoughTerrain_Sandy))
      .WithUserColor(Color.Yellow).From(22, 78)
      .ToBackground(Knowen.RoughTerrainSandy)
      .DefaultSystemTagSand()
      .WithMusic(Knowen.MusicGrass)
      .WithMapNamePanel(Knowen.PanelRoughTerrain)

      .WithShape<PalmTreeShape>().SpreadRandomly(13, 7, 3, 3).Added()
      .WithShape<CactusShape>().SpreadRandomly(8, 4, 0, 0).Added()
      .WithShape(new Shape(new string[] { TmxMapExportFormat.Layer1.Item2 }, new object[][,] {new object[,]{
          { 1 + 3259, 1 + 3259 },
          { 1 + 3259, 1 + 3259 },
      }})).SpreadRandomly(13,7, 3,3).Added()
      .WithShape(new Shape(new string[] { TmxMapExportFormat.Layer1.Item2 }, new object[][,] {new object[,]{
          { Knowen.Nothing, Knowen.Nothing },
          { Knowen.Nothing, Knowen.Nothing },
      }})).SpreadRandomly(8, 1, 0, 0).Added()

      .LimitedTo().Southern(30).Percent()
      ;

    public static DefinitivMapColor RoughTerrain =
      FluentNew.NewColor(nameof(RoughTerrain))
      .WithUserColor(Color.OrangeRed).From(22, 78)
      .ToBackground(Knowen.RoughTerrainRocky)
      .WithMusic(Knowen.MusicGrass)
      .WithMapNamePanel(Knowen.PanelRoughTerrain)

      // .WithShape<CrumbeledMessShape>().SpreadRandomly(4, 2, 7, 2).Added()
      .WithShape<CrumbeledMessShape>().SpreadRandomly(8, 4, 0, 0).Added()
      .WithShape<CrumbeledMessShape>().SpreadRandomly(13, 7, 3, 3).Added()
      .WithShape(Knowen.Rubble).SpreadRandomly(3, 7, 1, 2).Added()
      .WithShape(Knowen.Bolder).SpreadRandomly(1, 2, 2, 1).Added()
      .WithShape(Knowen.Rubble).SpreadRandomly(4, 7, 3, 3).Added()

      .AllAroundTheWorld()
      ;
    #endregion RoughTerrain

    #region Urban
    public static DefinitivMapColor Urban =
      FluentNew.NewColor(nameof(Urban))
      .WithUserColor(Color.Purple).From(250, 360)
      .ToBackground(Knowen.Uraban)
      .WithMapNamePanel(Knowen.PanelUrban)
      .WithBorder(Knowen.BorderUrban)
      .WithMusic(Knowen.MusicUraban)
      .WithShape<LampWestShape>().SpreadRandomly(10, 10, 10, 10).Added()
      .AllAroundTheWorld()
      ;

    public static DefinitivMapColor Urban_East =
      FluentNew.NewColor(nameof(Urban_East))
      .WithUserColor(Color.Purple).From(250, 360)
      .ToBackground(Knowen.Uraban)
      .WithMapNamePanel(Knowen.PanelUrban)
      .WithBorder(Knowen.BorderUrban)
      .WithMusic(Knowen.MusicUraban)
      .WithShape<LampEastShape>().SpreadRandomly(10, 10, 10, 10).Added()
      .LimitedTo().Eastern(10).Percent()
      ;
    #endregion Urban
  }
}
