namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
{
  using X = PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.DefinitivMapColor.FunctionParameters;

  public interface IShapePositon
  {
    bool IsStart(X parameters, Point start);
  }
}
