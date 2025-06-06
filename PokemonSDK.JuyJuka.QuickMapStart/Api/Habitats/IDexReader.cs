namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Habitats
{
  public interface IDexReader
  {
    IEnumerable<string> Read(string dexFile);
    bool? IsBaseForm(string dexFile, string pokemon);
  }
}