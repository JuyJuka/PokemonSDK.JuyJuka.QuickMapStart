namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes
{
  using System.Drawing;

  using X = PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.DefinitivMapColor.FunctionParameters;

  public class AtShapePositon : IShapePositon
  {
    public AtShapePositon() { }
    public AtShapePositon(int x, int y) { this.Point = new Point(x, y); }
    public AtShapePositon(Point point) { this.Point = point; }
    public virtual Point Point { get; set; } = new Point();
    public bool IsStart(X parameters, Point start)
    {
      return start.X == this.Point.X && start.Y == this.Point.Y;
    }
  }
}
