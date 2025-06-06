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
  public class SingleSurroundedShape : Shape
  {
    public SingleSurroundedShape(bool isBlocking, object grafic) : base(string.Empty
      , new object[,] { }
      , new object[,] {
        { _, _, _, },
        { _, isBlocking?X:_, _, },
        { _, _, _, },
      }
      , new object[,] { }
      , new object[,] { }
      , new object[,]{
        { _, _, _, },
        { _, grafic, _, },
        { _, _, _, },
      }
    )
    { }
  }
}
