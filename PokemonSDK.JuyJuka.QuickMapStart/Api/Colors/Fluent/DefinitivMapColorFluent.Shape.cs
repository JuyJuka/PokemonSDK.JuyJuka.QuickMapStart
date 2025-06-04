namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Fluent
{
  using System;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Colors.Shapes;

  partial class DefinitivMapColorFluent
  {
    public FluentShape1<T> WithShape<T>()
      where T : IShape, new()
    {
      return new FluentShape1<T>(this, new T());
    }

    public FluentShape1<IShape> WithShape(IShape shape)
    {
      return new FluentShape1<IShape>(this, shape ?? new StickPille1Shape());
    }

    public sealed class FluentShape1<T>
      where T : IShape
    {
      public FluentShape1(DefinitivMapColorFluent definitivMapColorFluent, T shape)
      {
        if (definitivMapColorFluent == null) throw new ArgumentNullException(nameof(definitivMapColorFluent));
        if (shape == null) throw new ArgumentNullException(nameof(shape));
        this._DefinitivMapColorFluent = definitivMapColorFluent;
        this._Shape = shape;
      }
      private DefinitivMapColorFluent _DefinitivMapColorFluent;
      private T _Shape;

      public FluentShape2<T> At(int x, int y)
      {
        this._Shape.Position = new AtShapePositon(x, y);
        return new FluentShape2<T>(this._DefinitivMapColorFluent, this._Shape);
      }

      public FluentShape2<T> At(Point point)
      {
        this._Shape.Position = new AtShapePositon(point);
        return new FluentShape2<T>(this._DefinitivMapColorFluent, this._Shape);
      }

      public FluentShape2<T> SpreadRandomly(int x, int y, int fx, int fy)
      {
        int maxY = Map._0;
        int maxX = Map._0;
        Shapes.Shape.Max((this._Shape as Shapes.Shape)?.Points, out maxX, out maxY);
        this._Shape.Position = new SeamingRandomShapePositon()
        {
          WorldMapCoordinatesXModulo = x,
          WorldMapCoordinatesYModulo = y,
          FrequencyX = Math.Max(fx, Map._0) + Math.Abs(maxX) + 1,
          FrequencyY = Math.Max(fy, Map._0) + Math.Abs(maxY) + 1,
        };
        return new FluentShape2<T>(this._DefinitivMapColorFluent, this._Shape);
      }
    }

    public sealed class FluentShape2<T>
      where T : IShape
    {
      public FluentShape2(DefinitivMapColorFluent definitivMapColorFluent, T shape)
      {
        if (definitivMapColorFluent == null) throw new ArgumentNullException(nameof(definitivMapColorFluent));
        if (shape == null) throw new ArgumentNullException(nameof(shape));
        this._DefinitivMapColorFluent = definitivMapColorFluent;
        this._Shape = shape;
      }
      private DefinitivMapColorFluent _DefinitivMapColorFluent;
      private T _Shape;

      public FluentShape2<T> With(Action<T> action)
      {
        if (action == null) action(this._Shape);
        return this;
      }

      public DefinitivMapColorFluent Added()
      {
        this._DefinitivMapColorFluent.DefinitivMapColor._Functions.Insert(Map._0, this._Shape.ToLayer);
        return this._DefinitivMapColorFluent;
      }
    }
  }
}