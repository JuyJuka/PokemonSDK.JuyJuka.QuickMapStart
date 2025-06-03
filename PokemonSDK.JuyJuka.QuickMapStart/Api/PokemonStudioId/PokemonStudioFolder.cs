namespace PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId
{
  public class PokemonStudioFolder: IPokemonStudioFolder
  {
    public static string Fallback = ".";
    public virtual string Folder { get; set; }
  }
}
