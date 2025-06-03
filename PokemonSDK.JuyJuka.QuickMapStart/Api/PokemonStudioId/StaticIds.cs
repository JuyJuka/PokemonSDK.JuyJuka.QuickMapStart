namespace PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;

  public static class StaticId
  {
    #region Docu?
    /*
export const MAP_NAME_TEXT_ID = 200002;
export const MAP_DESCRIPTION_TEXT_ID = 200003;
export const ZONE_DESCRIPTION_TEXT_ID = 100064;
export const ZONE_NAME_TEXT_ID = 100010;
     */
    #endregion Docu?

    public static IPokemonStudioId MapName = new MapIdShenanigangsPokemonStudioId(new PokemonStudioId() { Id = "200002", Name = "MAP_NAME_TEXT_ID" }, "Data", "Studio", "maps");
    public static IPokemonStudioId MapDescription = new PokemonStudioId() { Id = "200003", Name = "MAP_NAME_TEXT_ID" };
    public static IPokemonStudioId ZoneName = new PokemonStudioId() { Id = "100010", Name = "MAP_NAME_TEXT_ID" };
    public static IPokemonStudioId ZoneDescription = new PokemonStudioId() { Id = "100064", Name = "MAP_NAME_TEXT_ID" };
  }
}