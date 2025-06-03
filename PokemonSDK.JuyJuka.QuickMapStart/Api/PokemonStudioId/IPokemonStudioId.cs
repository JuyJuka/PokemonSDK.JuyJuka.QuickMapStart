namespace PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId
{
  public interface IPokemonStudioId
  {
    string Name { get; set; }
    string Id { get; set; }
    int GuessFor(IPokemonStudioFolder folder, object obj, bool withCache);
    void WriteText(IPokemonStudioFolder folder, object obj, bool withCache, string text);
    void WriteText(IPokemonStudioFolder folder, int id, string text);
  }
}
