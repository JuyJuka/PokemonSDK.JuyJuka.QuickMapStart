namespace PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations.Decorator;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations.MinMax;

  public class ColorEstimation : ListColorEstimation.Or<ColorEstimation<
    WorldMapCoordinatsPercentageColorEstimation<HueColorEstimation>,
    HueColorEstimation
    >>
  { }
}