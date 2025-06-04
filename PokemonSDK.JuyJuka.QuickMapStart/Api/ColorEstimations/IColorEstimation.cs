
namespace PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors;

  public interface IColorEstimation
  {
    IEnumerable<Tuple<IDefinitivMapColor, float>> Estimate(Map? map, Color color, IEnumerable<IDefinitivMapColor>? colors);
  }
}
