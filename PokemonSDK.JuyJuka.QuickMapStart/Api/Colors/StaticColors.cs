namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Fluent;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes;

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
      FluentNew.Color(nameof(Grassland))
      .From(Color.Green)
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
      FluentNew.Color(nameof(Forest))
      .FromRgb(0, 99, 20)
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
      FluentNew.Color(nameof(Forest_Sothern))
      .FromRgb(0, 99, 20)
      .ToBackground(Knowen.Grass)
      .WithMapNamePanel(Knowen.PanelForest)
      .WithMusic(Knowen.MusicGrass)
      .WithShape<PalmTreeShape>().SpreadRandomly(3, 9, 5, 7).Added()
      .WithShape<TreeShape>().SpreadRandomly(4, 10, 5, 7).Added()
      .WithShape<StickPille1Shape>().SpreadRandomly(4, 10, 5, 7).Added()
      .WithShape<StickPille2Shape>().SpreadRandomly(5, 9, 10, 3).Added()
      .WithShape<StickPille3Shape>().SpreadRandomly(5, 7, 5, 9).Added()
      .LimitedTo().Southern(30).Percent()
      ;
    #endregion Forest

    #region Sea
    public static DefinitivMapColor Sea =
      FluentNew.Color(nameof(Sea))
      .FromRgb(77, 105, 245)
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
      FluentNew.Color(nameof(Mountain))
      .FromRgb(125, 125, 140)
      .ToBackground(Knowen.Mointain)
      .WithBorder(Knowen.BorderMountain.Item1.Item1, Knowen.BorderMountain.Item1.Item2)
      .WithBorder(Knowen.BorderMountain.Item2.Item1, Knowen.BorderMountain.Item2.Item2)
      .WithBorder(Knowen.BorderMountain.Item3.Item1, Knowen.BorderMountain.Item3.Item2)
      .AllAroundTheWorld()
      ;

    public static DefinitivMapColor Mountain_Snowy =
      FluentNew.Color(nameof(Mountain_Snowy))
      .FromRgb(125, 125, 140)
      .ToBackground(Knowen.Mointain)
      .WithBorder(Knowen.BorderMountain.Item1.Item1, Knowen.BorderMountain.Item1.Item2)
      .WithBorder(Knowen.BorderMountain.Item2.Item1, Knowen.BorderMountain.Item2.Item2)
      .WithBorder(Knowen.BorderMountain.Item3.Item1, Knowen.BorderMountain.Item3.Item2)
      .WithShape<SnowPatchShape>().SpreadRandomly(13, 7, 3, 3).Added()
      .WithShape<SnowPatchShape>().SpreadRandomly(13, 7, 3, 3).Added()
      .AllAroundTheWorld()
      ;
    #endregion Mountain

    #region RoughTerrain
    public static DefinitivMapColor RoughTerrain_Sandy =
      FluentNew.Color(nameof(RoughTerrain_Sandy))
      .From(Color.Yellow)
      .ToBackground(Knowen.RoughTerrainSandy)
      .DefaultSystemTagSand()
      .WithMusic(Knowen.MusicGrass)
      .WithMapNamePanel(Knowen.PanelRoughTerrain)

      .WithShape<PalmTreeShape>().SpreadRandomly(4, 2, 10, 10).Added()
      .WithShape<CactusShape>().SpreadRandomly(13, 7, 3, 3).Added()

      .LimitedTo().Southern(30).Percent()
      ;

    public static DefinitivMapColor RoughTerrain =
      FluentNew.Color(nameof(RoughTerrain))
      .From(Color.Yellow)
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
      FluentNew.Color(nameof(Urban))
      .From(Color.LightGray)
      .ToBackground(Knowen.Uraban)
      .WithMapNamePanel(Knowen.PanelUrban)
      .WithBorder(Knowen.BorderUrban)
      .WithMusic(Knowen.MusicUraban)
      .WithShape<LampWestShape>().SpreadRandomly(10, 10, 10, 10).Added()
      .AllAroundTheWorld()
      ;

    public static DefinitivMapColor Urban_East =
      FluentNew.Color(nameof(Urban_East))
      .From(Color.LightGray)
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
