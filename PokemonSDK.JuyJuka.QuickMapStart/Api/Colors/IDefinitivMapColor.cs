namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Colors
{
  using System;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;

  public interface IDefinitivMapColor : ICloneable
  {
    Color Color { get; set; }
    float MinHue { get; set; }
    float MaxHue { get; set; }
    string Panel { get; set; }
    string Name { get; set; }
    string MusicName { get; set; }
    string ToLayer(Map map, int layerIndex, string layerName, Point point);
    PointF? WorldMapCoordinatsPercentageMin { get; set; }
    PointF? WorldMapCoordinatsPercentageMax { get; set; }
  }
}