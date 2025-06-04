namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
{
  public class SingleShape : Shape
  {
    public SingleShape(bool isBlocking, object grafic) : base(string.Empty
      , new object[,] { }
      , new object[,] {
        { isBlocking?X:_, },
      }
      , new object[,] { }
      , new object[,] { }
      , new object[,]{
        { grafic },
      }
    )
    { }
  }
}
