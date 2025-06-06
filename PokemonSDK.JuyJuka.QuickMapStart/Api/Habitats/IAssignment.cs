namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Habitats
{
  public interface IAssignment
  {
    IEnumerable<Tuple<Map, IDictionary<Habitat, IEnumerable<string>>>> Read(IEnumerable<Map> maps, string dexFile);
  }
}