namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports.Tiled
{
  using System;
  using System.Drawing;

  using PokemonSDK.JuyJuka.QuickMapStart.Api;
  using PokemonSDK.JuyJuka.QuickMapStart.Api.Exports;

  public class TmxMapExportFormat : SingleAssetMapExportFormat
  {
    private static Tuple<string, string> X(string a, string b) { return new Tuple<string, string>(a, b); }
    public static readonly string LayerP = "{{p}}";
    public static readonly string LayerS = "{{s}}";
    public static readonly string LayerTT = "{{tt}}";
    public static readonly Tuple<string, string> Layer3 = X("{{3_3}}", "{{3_d_3}}");
    public static readonly Tuple<string, string> Layer2 = X("{{2_2}}", "{{2_d_2}}");
    public static readonly Tuple<string, string> Layer1 = X("{{1_1}}", "{{1_d_1}}");

    public TmxMapExportFormat() : base(".tmx") { this.StaticFilter = "_tiled"; }

    public override string ModifyTargetFolder(Map map, string folder) { return Path.Combine(string.Empty + Path.GetDirectoryName(folder), Path.GetFileName(folder) + this.StaticFilter); }

    public override string Export(Map map, string folder, string file, string asset, string config)
    {
      //Draw(map, folder, file, asset, config);
      string re = asset;
      string[] layers = [
        LayerP,
        LayerS,
        LayerTT,
        Layer3.Item2,
        Layer3.Item1,
        Layer2.Item2,
        Layer2.Item1,
        Layer1.Item2,
        Layer1.Item1,
      ];
      string[] csvs = new string[layers.Length];

      for (int y = 0; y < map.World.Size.Height; y++)
      {
        for (int x = 0; x < map.World.Size.Width; x++)
        {
          for (int i = 0; i < layers.Length; i++)
          {
            csvs[i] += config + map.DefinitivColor.ToLayer(map, i, layers[i], new Point(x, y));
            if (x == map.World.Size.Width - Map._1) csvs[i] += Environment.NewLine;
          }
        }
      }
      for (int i = 0; i < layers.Length; i++)
      {
        re = re.Replace(layers[i], csvs[i].Length > Map._0 ? csvs[i].Substring(Map._1) : csvs[i]);
      }
      return re;
    }
  }
}