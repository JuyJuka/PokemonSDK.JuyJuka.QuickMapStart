namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports
{
  using System;

  using PokemonSDK.JuyJuka.QuickMapStart.Api.PokemonStudioId;

  public abstract class SingleAssetMapExportFormat : MapExportFormat
  {
    public SingleAssetMapExportFormat() : base() { }
    public SingleAssetMapExportFormat(params string[] pathAndExtension) : base(pathAndExtension) { }

    public override string Export(Map map, IPokemonStudioFolder project, string folder, string file, Func<string, Tuple<string, string>> readAsset)
    {
      Tuple<string, string> a = readAsset(this.GetType().Name);
      return this.Export(map, project, folder, file, a.Item2, a.Item1);
    }

    public abstract string Export(Map map, IPokemonStudioFolder project, string folder, string file, string asset, string config);
  }
}