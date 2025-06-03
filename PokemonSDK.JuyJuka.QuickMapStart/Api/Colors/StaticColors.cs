namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors
{
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
    // public static DefinitivMapColor DefinitivMapColors_WatersEdge = new DefinitivMapColor("WatersEdge",Color.Blue);
    // public static DefinitivMapColor DefinitivMapColors_Cave = new DefinitivMapColor("Cave",Color.Black);
    // public static DefinitivMapColor DefinitivMapColors_Rare = new DefinitivMapColor(Color.Red);
    #endregion Source Link Doku

    #region Grassland
    public static DefinitivMapColor DefinitivMapColors_Grassland =
      DefinitivMapColorFluent.New("Grassland", Color.Green, Knowen.Grass)
      .Panel(Knowen.PanelGrass)
      .Music(Knowen.MusicGrass)
      .DefinitivMapColor;
    #endregion Grassland

    #region Forest
    public static DefinitivMapColor DefinitivMapColors_Forest =
      DefinitivMapColorFluent.New("Forest", Color.FromArgb(0, 99, 20), Knowen.Grass)
      .Panel(Knowen.PanelForest)
      .Music(Knowen.MusicGrass)
      .DefinitivMapColor;
    #endregion Forest

    #region Sea
    public static DefinitivMapColor DefinitivMapColors_Sea =
      DefinitivMapColorFluent.New("Sea", Color.FromArgb(77, 105, 245), Knowen.Water)
      .DefaultSystemTagSea()
      .Panel(Knowen.PanelSea)
      .Border(Knowen.BorderSea)
      .Music(Knowen.MusicGrass)
      .DefinitivMapColor;
    #endregion Sea

    #region Mountain
    public static DefinitivMapColor DefinitivMapColors_Mountain =
      DefinitivMapColorFluent.New("Mountain", Color.FromArgb(125, 125, 140), Knowen.Mointain)
      .Panel(Knowen.PanelMointain)
      .Border(TmxMapExportFormat.Layer1.Item2, Knowen.BorderMountain)
      .Music(Knowen.MusicGrass)
      .Border(TmxMapExportFormat.LayerS, new Knowen.Border(
        Knowen.SystemTagClimb, Knowen.Nothing, Knowen.Nothing,
        Knowen.SystemTagClimb, Knowen.Nothing, Knowen.Nothing,
        Knowen.SystemTagClimb,
        Knowen.SystemTagClimb))
      .Border(TmxMapExportFormat.LayerP, new Knowen.Border(
        Knowen.PassagesX, Knowen.PassagesX, Knowen.PassagesX,
        Knowen.PassagesX, Knowen.PassagesX, Knowen.PassagesX,
        Knowen.PassagesX,
        Knowen.PassagesX))
      .DefinitivMapColor;
    #endregion Mountain

    #region RoughTerrain
    public static DefinitivMapColor DefinitivMapColors_RoughTerrain =
      DefinitivMapColorFluent.New("RoughTerrain", Color.Yellow, Knowen.RoughTerrain)
      .DefaultSystemTagSand()
      .Music(Knowen.MusicGrass)
      .Panel(Knowen.PanelRoughTerrain)
      .DefinitivMapColor;
    #endregion RoughTerrain

    #region Urban
    public static DefinitivMapColor DefinitivMapColors_Urban =
      DefinitivMapColorFluent.New("Urban", Color.LightGray, Knowen.Uraban)
      .Panel(Knowen.PanelUrban)
      .Border(Knowen.BorderUrban)
      .Music(Knowen.MusicUraban)
      .DefinitivMapColor;
    #endregion Urban
  }
}
