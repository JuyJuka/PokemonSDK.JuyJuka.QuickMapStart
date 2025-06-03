namespace PokemonSDK.JuyJuka.QuickMapStart.Api.Exports
{
  using System;

  public interface IMapExportFormat
  {
    string FileExtendsion { get; }
    string StaticFilter { get; }
    string ModifyTargetFolder(Map map, string folder);
    string ModifyTargetFile(Map map, string folder, string file);
    string Export(Map map, string folder, string file, Func<string, Tuple<string, string>> readAsset);
  }
}