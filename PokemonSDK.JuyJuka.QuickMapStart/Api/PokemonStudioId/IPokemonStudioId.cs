namespace PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId
{
  public interface IPokemonStudioId
  {
    string Name { get; set; }
    string Id { get; set; }
    int GuessFor(string folder, object obj, bool withCache);
  }
}
