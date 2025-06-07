namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Habitats
{
  public interface IAssignment
  {
    IHabitatDataBase HabitatData { get; }
    IEnumerable<Tuple<Map, IDictionary<Habitat, IEnumerable<string>>>> Read(IEnumerable<Map> maps, string dexFile);
  }
}