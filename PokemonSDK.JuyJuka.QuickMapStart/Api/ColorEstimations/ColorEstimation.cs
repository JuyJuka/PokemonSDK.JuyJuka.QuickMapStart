namespace PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations
{
  using PokemonSDK.JuyJuka.QuickMapStart.Api.ColorEstimations.Decorator;

  public class ColorEstimation : ListColorEstimation.Or<ColorEstimation<
    WorldMapCoordinatsPercentageColorEstimation<HueColorEstimation>,
    HueColorEstimation
    >>
  { }
}