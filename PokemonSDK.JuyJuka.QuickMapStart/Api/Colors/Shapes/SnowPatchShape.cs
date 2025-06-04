namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
{
  public class SnowPatchShape : Shape
  {
    public SnowPatchShape() : this(true) { }
    public SnowPatchShape(bool canEncounter) : base(string.Empty
      , (!canEncounter) ? (new object[,] { }) : new object[,]{
        { Knowen.SystemTagGrass, Knowen.SystemTagGrass, Knowen.SystemTagGrass },
        { Knowen.SystemTagGrass, Knowen.SystemTagGrass, Knowen.SystemTagGrass },
        { Knowen.SystemTagGrass, Knowen.SystemTagGrass, Knowen.SystemTagGrass },
      }
      , new object[,] { }
      , new object[,] { }
      , new object[,] { }
      , new object[,]{
        { 1+4228, 1+4229, 1+4230 },
        { 1+4236, 1+4237, 1+4238 },
        { 1+4244, 1+4245, 1+4246 },
      }
    )
    { }
  }
}
