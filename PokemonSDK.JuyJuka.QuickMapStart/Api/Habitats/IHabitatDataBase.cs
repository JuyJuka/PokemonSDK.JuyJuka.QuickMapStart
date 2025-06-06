namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Habitats
{
  public interface IHabitatDataBase
  {
    string Configuration { get; set; }
    Habitat this[string pokemon] { get; }
  }
}